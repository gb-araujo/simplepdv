using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimplePDV.WPF.Services;
using SimplePDV.WPF.Views;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace SimplePDV.WPF.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly UsuarioLocalService _usuarioService;
    private readonly ApiService _apiService;

    [ObservableProperty]
    private string login = string.Empty;

    [ObservableProperty]
    private string mensagemErro = string.Empty;

    [ObservableProperty]
    private bool isOnline;

    public LoginViewModel(UsuarioLocalService usuarioService, ApiService apiService)
    {
        _usuarioService = usuarioService;
        _apiService = apiService;
        VerificarConexaoAsync();
    }

    private async void VerificarConexaoAsync()
    {
        IsOnline = await _apiService.IsOnlineAsync();
    }

    [RelayCommand]
    private async Task LoginAsync(object parameter)
    {
        try
        {
            MensagemErro = string.Empty;
            
            if (string.IsNullOrWhiteSpace(Login))
            {
                MensagemErro = "Digite o login";
                return;
            }

            var passwordBox = parameter as System.Windows.Controls.PasswordBox;
            if (passwordBox == null || string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                MensagemErro = "Digite a senha";
                return;
            }

            var usuario = await _usuarioService.LoginAsync(Login, passwordBox.Password);
            if (usuario != null)
            {
                var mainWindow = App.ServiceProvider.GetRequiredService<MainWindow>();
                mainWindow.Show();
                
                // Fechar a janela de login
                foreach (Window window in Application.Current.Windows)
                {
                    if (window is LoginWindow)
                    {
                        window.Close();
                        break;
                    }
                }
            }
            else
            {
                MensagemErro = "Login ou senha inválidos";
            }
        }
        catch (Exception ex)
        {
            MensagemErro = $"Erro ao fazer login: {ex.Message}";
        }
    }
}
