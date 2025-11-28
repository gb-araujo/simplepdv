using SimplePDV.Domain.Entities;

namespace SimplePDV.Domain.Interfaces;

public interface IProdutoRepository : IRepository<Produto>
{
    Task<Produto?> GetBySKUAsync(string sku);
    Task<IEnumerable<Produto>> GetProdutosAtivosAsync();
    Task<IEnumerable<Produto>> GetProdutosEstoqueBaixoAsync();
}
