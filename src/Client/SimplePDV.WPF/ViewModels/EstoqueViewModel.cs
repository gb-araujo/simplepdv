using CommunityToolkit.Mvvm.ComponentModel;
using SimplePDV.Domain.Entities;
using SimplePDV.WPF.Services;
using System.Collections.ObjectModel;

namespace SimplePDV.WPF.ViewModels;

public partial class EstoqueViewModel : ObservableObject
{
    private readonly ProdutoLocalService _produtoService;

    [ObservableProperty]
    private ObservableCollection<Produto> produtos = new();

    public EstoqueViewModel(ProdutoLocalService produtoService)
    {
        _produtoService = produtoService;
        CarregarProdutosAsync();
    }

    private async void CarregarProdutosAsync()
    {
        var produtos = await _produtoService.GetAllAsync();
        Produtos.Clear();
        foreach (var produto in produtos)
        {
            Produtos.Add(produto);
        }
    }
}
