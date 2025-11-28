using SimplePDV.Domain.Enums;

namespace SimplePDV.Domain.Entities;

public class MovimentoEstoque : BaseEntity
{
    public int ProdutoId { get; set; }
    public Produto Produto { get; set; } = null!;
    
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
    
    public TipoMovimento Tipo { get; set; }
    public int Quantidade { get; set; }
    public int EstoqueAnterior { get; set; }
    public int EstoqueNovo { get; set; }
    public string? Observacao { get; set; }
    public DateTime DataMovimento { get; set; } = DateTime.Now;
    
    // Para rastreamento de vendas
    public int? VendaId { get; set; }
}
