namespace SimplePDV.Domain.Entities;

/// <summary>
/// Item individual de uma venda.
/// Guarda o preço no momento da venda pra não perder histórico se o preço do produto mudar.
/// </summary>
public class VendaItem : BaseEntity
{
    public int VendaId { get; set; }
    public Venda Venda { get; set; } = null!;
    
    public int ProdutoId { get; set; }
    public Produto Produto { get; set; } = null!;
    
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public decimal Subtotal { get; set; }
}
