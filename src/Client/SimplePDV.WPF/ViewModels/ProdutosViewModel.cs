using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimplePDV.Domain.Entities;
using SimplePDV.WPF.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace SimplePDV.WPF.ViewModels;

public partial class ProdutosViewModel : ObservableObject
{
    private readonly ProdutoLocalService _produtoService;
    private readonly ApiService _apiService;
    private readonly SincronizacaoService _sincronizacaoService;

    [ObservableProperty]
    private ObservableCollection<Produto> produtos = new();

    [ObservableProperty]
    private Produto? produtoSelecionado;

    [ObservableProperty]
    private string filtroSKU = string.Empty;

    [ObservableProperty]
    private bool isOnline;

    public ProdutosViewModel(ProdutoLocalService produtoService, ApiService apiService, SincronizacaoService sincronizacaoService)
    {
        _produtoService = produtoService;
        _apiService = apiService;
        _sincronizacaoService = sincronizacaoService;
        _ = InicializarAsync();
    }

    private async Task InicializarAsync()
    {
        IsOnline = await _apiService.IsOnlineAsync();
        await CarregarProdutosAsync();
    }

    [RelayCommand]
    private async Task CarregarProdutosAsync()
    {
        var produtos = await _produtoService.GetAllAsync();
        Produtos.Clear();
        foreach (var produto in produtos)
        {
            Produtos.Add(produto);
        }
    }

    [RelayCommand]
    private async Task BuscarPorSKUAsync()
    {
        if (string.IsNullOrWhiteSpace(FiltroSKU))
        {
            await CarregarProdutosAsync();
            return;
        }

        var produto = await _produtoService.GetBySKUAsync(FiltroSKU);
        Produtos.Clear();
        if (produto != null)
        {
            Produtos.Add(produto);
        }
    }

    [RelayCommand]
    private async Task ExcluirProdutoAsync()
    {
        if (ProdutoSelecionado == null)
        {
            MessageBox.Show("Selecione um produto para inativar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var result = MessageBox.Show(
            $"Deseja realmente inativar o produto '{ProdutoSelecionado.Nome}'?\nO produto ficará oculto mas poderá ser reativado posteriormente.",
            "Confirmar Inativação",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (result != MessageBoxResult.Yes)
            return;

        try
        {
            // Inativar na API se estiver online
            if (IsOnline)
            {
                var sucesso = await _apiService.DeleteAsync($"produtos/{ProdutoSelecionado.Id}");
                if (!sucesso)
                {
                    MessageBox.Show("Erro ao inativar produto na API.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            // Inativar no banco local
            await _produtoService.InativarAsync(ProdutoSelecionado.Id);

            // Remover da coleção (interface) pois só mostra ativos
            Produtos.Remove(ProdutoSelecionado);
            ProdutoSelecionado = null;

            MessageBox.Show("Produto inativado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao inativar produto: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private async Task SincronizarAsync()
    {
        try
        {
            await _sincronizacaoService.SincronizarAsync();
            await CarregarProdutosAsync();
            IsOnline = await _apiService.IsOnlineAsync();
            MessageBox.Show("Sincronização concluída!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro na sincronização: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
