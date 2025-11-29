using Microsoft.EntityFrameworkCore;
using SimplePDV.Domain.Entities;
using SimplePDV.WPF.Data;

namespace SimplePDV.WPF.Services;

public class ProdutoLocalService
{
    private readonly LocalDbContext _context;

    public ProdutoLocalService(LocalDbContext context)
    {
        _context = context;
    }

    public async Task<List<Produto>> GetAllAsync()
    {
        return await _context.Produtos.Where(p => p.Ativo).ToListAsync();
    }

    public async Task<Produto?> GetByIdAsync(int id)
    {
        return await _context.Produtos.FindAsync(id);
    }

    public async Task<Produto?> GetBySKUAsync(string sku)
    {
        return await _context.Produtos.FirstOrDefaultAsync(p => p.SKU == sku);
    }

    public async Task<Produto> AddOrUpdateAsync(Produto produto)
    {
        var existing = await _context.Produtos.FindAsync(produto.Id);
        if (existing != null)
        {
            _context.Entry(existing).CurrentValues.SetValues(produto);
        }
        else
        {
            _context.Produtos.Add(produto);
        }
        await _context.SaveChangesAsync();
        return produto;
    }

    public async Task AtualizarEstoqueAsync(int produtoId, int quantidade)
    {
        var produto = await _context.Produtos.FindAsync(produtoId);
        if (produto != null)
        {
            produto.EstoqueAtual -= quantidade;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> InativarAsync(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto != null)
        {
            produto.Ativo = false;
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task SincronizarProdutosAsync(List<Produto> produtos)
    {
        foreach (var produto in produtos)
        {
            await AddOrUpdateAsync(produto);
        }
    }
}
