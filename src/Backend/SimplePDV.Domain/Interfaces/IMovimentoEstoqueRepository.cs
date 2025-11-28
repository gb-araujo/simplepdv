using SimplePDV.Domain.Entities;

namespace SimplePDV.Domain.Interfaces;

public interface IMovimentoEstoqueRepository : IRepository<MovimentoEstoque>
{
    Task<IEnumerable<MovimentoEstoque>> GetMovimentosPorProdutoAsync(int produtoId);
    Task<IEnumerable<MovimentoEstoque>> GetMovimentosPorPeriodoAsync(DateTime dataInicio, DateTime dataFim);
}
