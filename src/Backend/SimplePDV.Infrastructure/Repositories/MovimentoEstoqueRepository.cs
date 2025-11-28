using Microsoft.EntityFrameworkCore;
using SimplePDV.Domain.Entities;
using SimplePDV.Domain.Interfaces;
using SimplePDV.Infrastructure.Data;

namespace SimplePDV.Infrastructure.Repositories;

public class MovimentoEstoqueRepository : Repository<MovimentoEstoque>, IMovimentoEstoqueRepository
{
    public MovimentoEstoqueRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<MovimentoEstoque>> GetMovimentosPorProdutoAsync(int produtoId)
    {
        return await _dbSet
            .Include(m => m.Produto)
            .Include(m => m.Usuario)
            .Where(m => m.ProdutoId == produtoId)
            .OrderByDescending(m => m.DataMovimento)
            .ToListAsync();
    }

    public async Task<IEnumerable<MovimentoEstoque>> GetMovimentosPorPeriodoAsync(DateTime dataInicio, DateTime dataFim)
    {
        return await _dbSet
            .Include(m => m.Produto)
            .Include(m => m.Usuario)
            .Where(m => m.DataMovimento >= dataInicio && m.DataMovimento <= dataFim)
            .OrderByDescending(m => m.DataMovimento)
            .ToListAsync();
    }
}
