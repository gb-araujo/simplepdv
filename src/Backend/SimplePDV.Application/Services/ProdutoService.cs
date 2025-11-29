using SimplePDV.Application.DTOs;
using SimplePDV.Domain.Entities;
using SimplePDV.Domain.Interfaces;

namespace SimplePDV.Application.Services;

public class ProdutoService
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<IEnumerable<ProdutoDto>> GetAllAsync()
    {
        var produtos = await _produtoRepository.GetAllAsync();
        return produtos.Select(MapToDto);
    }

    public async Task<IEnumerable<ProdutoDto>> GetAtivosAsync()
    {
        var produtos = await _produtoRepository.GetProdutosAtivosAsync();
        return produtos.Select(MapToDto);
    }

    public async Task<ProdutoDto?> GetByIdAsync(int id)
    {
        var produto = await _produtoRepository.GetByIdAsync(id);
        return produto != null ? MapToDto(produto) : null;
    }

    public async Task<ProdutoDto?> GetBySKUAsync(string sku)
    {
        var produto = await _produtoRepository.GetBySKUAsync(sku);
        return produto != null ? MapToDto(produto) : null;
    }

    public async Task<ProdutoDto> CreateAsync(ProdutoCreateDto dto)
    {
        var produtoExistente = await _produtoRepository.GetBySKUAsync(dto.SKU);
        if (produtoExistente != null)
            throw new Exception("Já existe um produto com este SKU");

        var produto = new Produto
        {
            Nome = dto.Nome,
            SKU = dto.SKU,
            Preco = dto.Preco,
            EstoqueAtual = dto.EstoqueAtual,
            EstoqueMinimo = dto.EstoqueMinimo,
            Ativo = true
        };

        await _produtoRepository.AddAsync(produto);
        return MapToDto(produto);
    }

    public async Task<ProdutoDto> UpdateAsync(int id, ProdutoUpdateDto dto)
    {
        var produto = await _produtoRepository.GetByIdAsync(id);
        if (produto == null)
            throw new Exception("Produto não encontrado");

        produto.Nome = dto.Nome;
        produto.Preco = dto.Preco;
        produto.EstoqueMinimo = dto.EstoqueMinimo;
        produto.Ativo = dto.Ativo;

        await _produtoRepository.UpdateAsync(produto);
        return MapToDto(produto);
    }

    public async Task DeleteAsync(int id)
    {
        var produto = await _produtoRepository.GetByIdAsync(id);
        if (produto == null)
            throw new Exception("Produto não encontrado");

        produto.Ativo = false;
        await _produtoRepository.UpdateAsync(produto);
    }

    public async Task<IEnumerable<ProdutoDto>> GetProdutosEstoqueBaixoAsync()
    {
        var produtos = await _produtoRepository.GetProdutosEstoqueBaixoAsync();
        return produtos.Select(MapToDto);
    }

    private static ProdutoDto MapToDto(Produto produto)
    {
        return new ProdutoDto
        {
            Id = produto.Id,
            Nome = produto.Nome,
            SKU = produto.SKU,
            Preco = produto.Preco,
            EstoqueAtual = produto.EstoqueAtual,
            EstoqueMinimo = produto.EstoqueMinimo,
            Ativo = produto.Ativo
        };
    }
}
