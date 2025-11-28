using Microsoft.EntityFrameworkCore;
using SimplePDV.Domain.Entities;
using SimplePDV.Domain.Interfaces;
using SimplePDV.Infrastructure.Data;

namespace SimplePDV.Infrastructure.Repositories;

public class VendaRepository : Repository<Venda>, IVendaRepository
{
    public VendaRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Venda>> GetVendasPorPeriodoAsync(DateTime dataInicio, DateTime dataFim)
    {
        return await _dbSet
            .Include(v => v.Itens)
            .ThenInclude(i => i.Produto)
            .Include(v => v.Usuario)
            .Where(v => v.DataVenda >= dataInicio && v.DataVenda <= dataFim)
            .OrderByDescending(v => v.DataVenda)
            .ToListAsync();
    }

    public async Task<IEnumerable<Venda>> GetVendasNaoSincronizadasAsync()
    {
        return await _dbSet
            .Include(v => v.Itens)
            .ThenInclude(i => i.Produto)
            .Where(v => !v.Sincronizado)
            .ToListAsync();
    }

    public async Task<Venda?> GetVendaComItensAsync(int id)
    {
        return await _dbSet
            .Include(v => v.Itens)
            .ThenInclude(i => i.Produto)
            .Include(v => v.Usuario)
            .FirstOrDefaultAsync(v => v.Id == id);
    }
}
