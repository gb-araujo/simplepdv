using Microsoft.EntityFrameworkCore;
using SimplePDV.Domain.Entities;
using SimplePDV.Domain.Interfaces;
using SimplePDV.Infrastructure.Data;

namespace SimplePDV.Infrastructure.Repositories;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Produto?> GetBySKUAsync(string sku)
    {
        return await _dbSet.FirstOrDefaultAsync(p => p.SKU == sku);
    }

    public async Task<IEnumerable<Produto>> GetProdutosAtivosAsync()
    {
        return await _dbSet.Where(p => p.Ativo).ToListAsync();
    }

    public async Task<IEnumerable<Produto>> GetProdutosEstoqueBaixoAsync()
    {
        return await _dbSet
            .Where(p => p.Ativo && p.EstoqueAtual <= p.EstoqueMinimo)
            .ToListAsync();
    }
}
