namespace SimplePDV.Domain.Entities;

public class Usuario : BaseEntity
{
    public string Nome { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;
    public bool Ativo { get; set; } = true;
    
    // Relacionamentos
    public ICollection<Venda> Vendas { get; set; } = new List<Venda>();
    public ICollection<MovimentoEstoque> MovimentosEstoque { get; set; } = new List<MovimentoEstoque>();
}
