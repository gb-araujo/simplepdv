using SimplePDV.Application.DTOs;
using SimplePDV.Domain.Entities;
using SimplePDV.Domain.Enums;
using SimplePDV.Domain.Interfaces;

namespace SimplePDV.Application.Services;

public class VendaService
{
    private readonly IVendaRepository _vendaRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMovimentoEstoqueRepository _movimentoEstoqueRepository;

    public VendaService(
        IVendaRepository vendaRepository,
        IProdutoRepository produtoRepository,
        IMovimentoEstoqueRepository movimentoEstoqueRepository)
    {
        _vendaRepository = vendaRepository;
        _produtoRepository = produtoRepository;
        _movimentoEstoqueRepository = movimentoEstoqueRepository;
    }

    public async Task<IEnumerable<VendaDto>> GetAllAsync()
    {
        var vendas = await _vendaRepository.GetAllAsync();
        var vendasComItens = new List<VendaDto>();
        
        foreach (var venda in vendas)
        {
            var vendaCompleta = await _vendaRepository.GetVendaComItensAsync(venda.Id);
            if (vendaCompleta != null)
                vendasComItens.Add(MapToDto(vendaCompleta));
        }
        
        return vendasComItens;
    }

    public async Task<VendaDto?> GetByIdAsync(int id)
    {
        var venda = await _vendaRepository.GetVendaComItensAsync(id);
        return venda != null ? MapToDto(venda) : null;
    }

    public async Task<IEnumerable<VendaDto>> GetVendasPorPeriodoAsync(DateTime dataInicio, DateTime dataFim)
    {
        var vendas = await _vendaRepository.GetVendasPorPeriodoAsync(dataInicio, dataFim);
        return vendas.Select(MapToDto);
    }

    public async Task<VendaDto> CreateAsync(VendaCreateDto dto)
    {
        // Validar estoque
        foreach (var item in dto.Itens)
        {
            var produto = await _produtoRepository.GetByIdAsync(item.ProdutoId);
            if (produto == null)
                throw new Exception($"Produto {item.ProdutoId} não encontrado");
            
            if (produto.EstoqueAtual < item.Quantidade)
                throw new Exception($"Estoque insuficiente para o produto {produto.Nome}");
        }

        // Criar venda
        var venda = new Venda
        {
            UsuarioId = dto.UsuarioId,
            DataVenda = DateTime.Now,
            FormaPagamento = dto.FormaPagamento,
            Itens = new List<VendaItem>()
        };

        decimal valorTotal = 0;

        // Adicionar itens e atualizar estoque
        foreach (var itemDto in dto.Itens)
        {
            var produto = await _produtoRepository.GetByIdAsync(itemDto.ProdutoId);
            if (produto == null) continue;

            var subtotal = produto.Preco * itemDto.Quantidade;
            valorTotal += subtotal;

            var item = new VendaItem
            {
                ProdutoId = itemDto.ProdutoId,
                Quantidade = itemDto.Quantidade,
                PrecoUnitario = produto.Preco,
                Subtotal = subtotal
            };

            venda.Itens.Add(item);

            // Atualizar estoque
            var estoqueAnterior = produto.EstoqueAtual;
            produto.EstoqueAtual -= itemDto.Quantidade;
            await _produtoRepository.UpdateAsync(produto);

            // Registrar movimento de estoque
            var movimento = new MovimentoEstoque
            {
                ProdutoId = produto.Id,
                UsuarioId = dto.UsuarioId,
                Tipo = TipoMovimento.Venda,
                Quantidade = itemDto.Quantidade,
                EstoqueAnterior = estoqueAnterior,
                EstoqueNovo = produto.EstoqueAtual,
                DataMovimento = DateTime.Now
            };
            await _movimentoEstoqueRepository.AddAsync(movimento);
        }

        venda.ValorTotal = valorTotal;
        await _vendaRepository.AddAsync(venda);

        return MapToDto(await _vendaRepository.GetVendaComItensAsync(venda.Id) ?? venda);
    }

    public async Task<IEnumerable<VendaDto>> GetVendasNaoSincronizadasAsync()
    {
        var vendas = await _vendaRepository.GetVendasNaoSincronizadasAsync();
        return vendas.Select(MapToDto);
    }

    public async Task MarcarComoSincronizadaAsync(int id)
    {
        var venda = await _vendaRepository.GetByIdAsync(id);
        if (venda != null)
        {
            venda.Sincronizado = true;
            await _vendaRepository.UpdateAsync(venda);
        }
    }

    private static VendaDto MapToDto(Venda venda)
    {
        return new VendaDto
        {
            Id = venda.Id,
            UsuarioId = venda.UsuarioId,
            UsuarioNome = venda.Usuario?.Nome ?? "",
            DataVenda = venda.DataVenda,
            ValorTotal = venda.ValorTotal,
            FormaPagamento = venda.FormaPagamento,
            Itens = venda.Itens.Select(i => new VendaItemDto
            {
                Id = i.Id,
                ProdutoId = i.ProdutoId,
                ProdutoNome = i.Produto?.Nome ?? "",
                Quantidade = i.Quantidade,
                PrecoUnitario = i.PrecoUnitario,
                Subtotal = i.Subtotal
            }).ToList()
        };
    }
}
