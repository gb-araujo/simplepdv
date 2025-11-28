namespace SimplePDV.Domain.Entities;

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
