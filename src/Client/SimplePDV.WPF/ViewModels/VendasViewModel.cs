using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimplePDV.Domain.Entities;
using SimplePDV.Domain.Enums;
using SimplePDV.WPF.Services;
using System.Collections.ObjectModel;

namespace SimplePDV.WPF.ViewModels;

public partial class VendasViewModel : ObservableObject
{
    private readonly VendaLocalService _vendaService;
    private readonly ProdutoLocalService _produtoService;

    [ObservableProperty]
    private ObservableCollection<VendaItem> itensVenda = new();

    [ObservableProperty]
    private ObservableCollection<Produto> produtos = new();

    [ObservableProperty]
    private Produto? produtoSelecionado;

    [ObservableProperty]
    private int quantidade = 1;

    [ObservableProperty]
    private decimal total = 0;

    [ObservableProperty]
    private FormaPagamento formaPagamentoSelecionada = FormaPagamento.Dinheiro;

    public Array FormasPagamento => Enum.GetValues(typeof(FormaPagamento));

    public VendasViewModel(VendaLocalService vendaService, ProdutoLocalService produtoService)
    {
        _vendaService = vendaService;
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

    [RelayCommand]
    private void AdicionarItem()
    {
        if (ProdutoSelecionado == null || Quantidade <= 0)
            return;

        if (ProdutoSelecionado.EstoqueAtual < Quantidade)
        {
            System.Windows.MessageBox.Show("Estoque insuficiente!", "Aviso", 
                System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
            return;
        }

        var item = new VendaItem
        {
            ProdutoId = ProdutoSelecionado.Id,
            Produto = ProdutoSelecionado,
            Quantidade = Quantidade,
            PrecoUnitario = ProdutoSelecionado.Preco,
            Subtotal = ProdutoSelecionado.Preco * Quantidade
        };

        ItensVenda.Add(item);
        Total += item.Subtotal;
        Quantidade = 1;
    }

    [RelayCommand]
    private void RemoverItem(VendaItem item)
    {
        ItensVenda.Remove(item);
        Total -= item.Subtotal;
    }

    [RelayCommand]
    private async Task FinalizarVendaAsync()
    {
        if (!ItensVenda.Any())
        {
            System.Windows.MessageBox.Show("Adicione itens à venda!", "Aviso",
                System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
            return;
        }

        var venda = new Venda
        {
            UsuarioId = 1, // TODO: Obter do usuário logado
            DataVenda = DateTime.Now,
            ValorTotal = Total,
            FormaPagamento = FormaPagamentoSelecionada,
            Itens = ItensVenda.ToList()
        };

        try
        {
            await _vendaService.CreateAsync(venda);
            System.Windows.MessageBox.Show("Venda finalizada com sucesso!", "Sucesso",
                System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            
            ItensVenda.Clear();
            Total = 0;
            CarregarProdutosAsync();
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show($"Erro ao finalizar venda: {ex.Message}", "Erro",
                System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private void CancelarVenda()
    {
        ItensVenda.Clear();
        Total = 0;
    }
}
