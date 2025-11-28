using SimplePDV.Domain.Entities;

namespace SimplePDV.Domain.Interfaces;

public interface IVendaRepository : IRepository<Venda>
{
    Task<IEnumerable<Venda>> GetVendasPorPeriodoAsync(DateTime dataInicio, DateTime dataFim);
    Task<IEnumerable<Venda>> GetVendasNaoSincronizadasAsync();
    Task<Venda?> GetVendaComItensAsync(int id);
}
