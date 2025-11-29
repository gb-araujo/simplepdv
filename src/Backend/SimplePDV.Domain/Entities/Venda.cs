using SimplePDV.Domain.Enums;

namespace SimplePDV.Domain.Entities;

/// <summary>
/// Representa uma venda realizada no sistema.
/// TODO: Adicionar campo para desconto e campo de observações
/// </summary>
public class Venda : BaseEntity
{
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
    
    public DateTime DataVenda { get; set; } = DateTime.Now;
    public decimal ValorTotal { get; set; }
    public FormaPagamento FormaPagamento { get; set; }
    
    // Flag para controle de sincronização offline (ainda não implementado)
    public bool Sincronizado { get; set; } = false;
    
    public ICollection<VendaItem> Itens { get; set; } = new List<VendaItem>();
}
