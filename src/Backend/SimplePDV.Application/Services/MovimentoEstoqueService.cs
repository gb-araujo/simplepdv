using SimplePDV.Application.DTOs;
using SimplePDV.Domain.Entities;
using SimplePDV.Domain.Enums;
using SimplePDV.Domain.Interfaces;

namespace SimplePDV.Application.Services;

public class MovimentoEstoqueService
{
    private readonly IMovimentoEstoqueRepository _movimentoRepository;
    private readonly IProdutoRepository _produtoRepository;

    public MovimentoEstoqueService(
        IMovimentoEstoqueRepository movimentoRepository,
        IProdutoRepository produtoRepository)
    {
        _movimentoRepository = movimentoRepository;
        _produtoRepository = produtoRepository;
    }

    public async Task<IEnumerable<MovimentoEstoqueDto>> GetMovimentosPorProdutoAsync(int produtoId)
    {
        var movimentos = await _movimentoRepository.GetMovimentosPorProdutoAsync(produtoId);
        return movimentos.Select(MapToDto);
    }

    public async Task<IEnumerable<MovimentoEstoqueDto>> GetMovimentosPorPeriodoAsync(DateTime dataInicio, DateTime dataFim)
    {
        var movimentos = await _movimentoRepository.GetMovimentosPorPeriodoAsync(dataInicio, dataFim);
        return movimentos.Select(MapToDto);
    }

    public async Task<MovimentoEstoqueDto> CreateAsync(MovimentoEstoqueCreateDto dto)
    {
        var produto = await _produtoRepository.GetByIdAsync(dto.ProdutoId);
        if (produto == null)
            throw new Exception("Produto não encontrado");

        var estoqueAnterior = produto.EstoqueAtual;
        var estoqueNovo = estoqueAnterior;

        // Calcular novo estoque baseado no tipo de movimento
        switch (dto.Tipo)
        {
            case TipoMovimento.Entrada:
                estoqueNovo += dto.Quantidade;
                break;
            case TipoMovimento.Saida:
                estoqueNovo -= dto.Quantidade;
                if (estoqueNovo < 0)
                    throw new Exception("Estoque insuficiente");
                break;
            case TipoMovimento.AjusteManual:
                estoqueNovo = dto.Quantidade; // Quantidade é o valor absoluto do novo estoque
                break;
        }

        var movimento = new MovimentoEstoque
        {
            ProdutoId = dto.ProdutoId,
            UsuarioId = dto.UsuarioId,
            Tipo = dto.Tipo,
            Quantidade = dto.Tipo == TipoMovimento.AjusteManual ? estoqueNovo - estoqueAnterior : dto.Quantidade,
            EstoqueAnterior = estoqueAnterior,
            EstoqueNovo = estoqueNovo,
            Observacao = dto.Observacao,
            DataMovimento = DateTime.Now
        };

        produto.EstoqueAtual = estoqueNovo;
        await _produtoRepository.UpdateAsync(produto);
        await _movimentoRepository.AddAsync(movimento);

        return MapToDto(movimento);
    }

    private static MovimentoEstoqueDto MapToDto(MovimentoEstoque movimento)
    {
        return new MovimentoEstoqueDto
        {
            Id = movimento.Id,
            ProdutoId = movimento.ProdutoId,
            ProdutoNome = movimento.Produto?.Nome ?? "",
            UsuarioId = movimento.UsuarioId,
            UsuarioNome = movimento.Usuario?.Nome ?? "",
            Tipo = movimento.Tipo,
            Quantidade = movimento.Quantidade,
            EstoqueAnterior = movimento.EstoqueAnterior,
            EstoqueNovo = movimento.EstoqueNovo,
            Observacao = movimento.Observacao,
            DataMovimento = movimento.DataMovimento
        };
    }
}
