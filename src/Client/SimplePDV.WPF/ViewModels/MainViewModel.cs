using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimplePDV.WPF.Services;
using System.Windows.Controls;

namespace SimplePDV.WPF.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly SincronizacaoService _sincronizacaoService;

    [ObservableProperty]
    private UserControl? currentViewModel;

    [ObservableProperty]
    private bool isOnline;

    [ObservableProperty]
    private string statusSincronizacao = "Desconectado";

    public MainViewModel(SincronizacaoService sincronizacaoService)
    {
        _sincronizacaoService = sincronizacaoService;
        VerificarConexaoAsync();
    }

    private async void VerificarConexaoAsync()
    {
        try
        {
            System.Diagnostics.Debug.WriteLine("Iniciando verificação de conexão...");
            IsOnline = await _sincronizacaoService.SincronizarAsync();
            StatusSincronizacao = IsOnline ? "Online - Sincronizado" : "Offline";
            System.Diagnostics.Debug.WriteLine($"Resultado: {StatusSincronizacao}");
        }
        catch (Exception ex)
        {
            StatusSincronizacao = $"Erro: {ex.Message}";
            System.Diagnostics.Debug.WriteLine($"Erro na verificação: {ex}");
        }
    }

    [RelayCommand]
    private async Task SincronizarAsync()
    {
        try
        {
            StatusSincronizacao = "Sincronizando...";
            IsOnline = await _sincronizacaoService.SincronizarAsync();
            StatusSincronizacao = IsOnline ? "Online - Sincronizado" : "Offline - Falha na sincronização";
            
            if (IsOnline)
            {
                System.Windows.MessageBox.Show("Sincronização concluída com sucesso!", "Sucesso", 
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }
            else
            {
                System.Windows.MessageBox.Show("Não foi possível conectar com o servidor. Verifique se a API está rodando.", "Erro", 
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
            }
        }
        catch (Exception ex)
        {
            StatusSincronizacao = $"Erro: {ex.Message}";
            System.Windows.MessageBox.Show($"Erro ao sincronizar: {ex.Message}", "Erro", 
                System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private void NavigateToVendas()
    {
        var viewModel = App.ServiceProvider.GetService(typeof(VendasViewModel)) as VendasViewModel;
        var view = App.ServiceProvider.GetService(typeof(Views.VendasView)) as Views.VendasView;
        if (view != null && viewModel != null)
        {
            view.DataContext = viewModel;
            CurrentViewModel = view;
        }
    }

    [RelayCommand]
    private void NavigateToProdutos()
    {
        var viewModel = App.ServiceProvider.GetService(typeof(ProdutosViewModel)) as ProdutosViewModel;
        var view = App.ServiceProvider.GetService(typeof(Views.ProdutosView)) as Views.ProdutosView;
        if (view != null && viewModel != null)
        {
            view.DataContext = viewModel;
            CurrentViewModel = view;
        }
    }

    [RelayCommand]
    private void NavigateToEstoque()
    {
        var viewModel = App.ServiceProvider.GetService(typeof(EstoqueViewModel)) as EstoqueViewModel;
        var view = App.ServiceProvider.GetService(typeof(Views.EstoqueView)) as Views.EstoqueView;
        if (view != null && viewModel != null)
        {
            view.DataContext = viewModel;
            CurrentViewModel = view;
        }
    }
}
