using SimplePDV.Domain.Enums;

namespace SimplePDV.Domain.Entities;

public class Venda : BaseEntity
{
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
    
    public DateTime DataVenda { get; set; } = DateTime.Now;
    public decimal ValorTotal { get; set; }
    public FormaPagamento FormaPagamento { get; set; }
    public bool Sincronizado { get; set; } = false;
    
    // Relacionamentos
    public ICollection<VendaItem> Itens { get; set; } = new List<VendaItem>();
}
