namespace SimplePDV.Domain.Entities;

/// <summary>
/// Produto comercializado no sistema.
/// Optei por usar inativação (Ativo=false) em vez de exclusão física
/// pra manter histórico de vendas íntegro.
/// </summary>
public class Produto : BaseEntity
{
    public string SKU { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public int EstoqueAtual { get; set; }
    public int EstoqueMinimo { get; set; } = 0;
    public bool Ativo { get; set; } = true;
    
    // Relacionamentos
    public ICollection<VendaItem> VendaItens { get; set; } = new List<VendaItem>();
    public ICollection<MovimentoEstoque> MovimentosEstoque { get; set; } = new List<MovimentoEstoque>();
}
