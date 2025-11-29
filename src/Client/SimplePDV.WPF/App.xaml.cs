using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using SimplePDV.WPF.Data;
using SimplePDV.WPF.Services;
using SimplePDV.WPF.ViewModels;
using SimplePDV.WPF.Views;
using System.IO;
using SimplePDV.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SimplePDV.WPF;

public partial class App : Application
{
    public static IServiceProvider ServiceProvider { get; private set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        try
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            // Inicializar banco local
            var dbContext = ServiceProvider.GetRequiredService<LocalDbContext>();
            dbContext.Database.EnsureCreated();

            // Criar usuário admin padrão se não existir
            InicializarUsuarioAdmin(dbContext);

            var loginWindow = ServiceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
        }
        catch (Exception ex)
        {
            var logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), 
                "SimplePDV", "error.log");
            Directory.CreateDirectory(Path.GetDirectoryName(logPath)!);
            File.WriteAllText(logPath, $"{DateTime.Now}: {ex}\n");
            
            MessageBox.Show($"Erro ao iniciar aplicativo:\n\n{ex.Message}\n\nDetalhes salvos em: {logPath}", 
                "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            Shutdown();
        }
    }

    private void InicializarUsuarioAdmin(LocalDbContext dbContext)
    {
        // Verifica se já existe usuário admin
        if (!dbContext.Usuarios.Any(u => u.Login == "admin"))
        {
            var adminUser = new Usuario
            {
                Nome = "Administrador",
                Login = "admin",
                SenhaHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                Ativo = true,
                CriadoEm = DateTime.Now
            };
            dbContext.Usuarios.Add(adminUser);
            dbContext.SaveChanges();
        }
    }

    private void ConfigureServices(IServiceCollection services)
    {
        // Database
        services.AddSingleton<LocalDbContext>();

        // Services
        services.AddSingleton<ApiService>();
        services.AddSingleton<ProdutoLocalService>();
        services.AddSingleton<VendaLocalService>();
        services.AddSingleton<SincronizacaoService>();
        services.AddSingleton<UsuarioLocalService>();

        // ViewModels
        services.AddTransient<LoginViewModel>();
        services.AddTransient<MainViewModel>();
        services.AddTransient<ProdutosViewModel>();
        services.AddTransient<VendasViewModel>();
        services.AddTransient<EstoqueViewModel>();

        // Views
        services.AddTransient<LoginWindow>();
        services.AddTransient<MainWindow>();
        services.AddTransient<Views.VendasView>();
        services.AddTransient<Views.ProdutosView>();
        services.AddTransient<Views.EstoqueView>();
    }
}
