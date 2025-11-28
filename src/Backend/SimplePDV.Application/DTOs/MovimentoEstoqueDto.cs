using SimplePDV.Domain.Enums;

namespace SimplePDV.Application.DTOs;

public class MovimentoEstoqueDto
{
    public int Id { get; set; }
    public int ProdutoId { get; set; }
    public string ProdutoNome { get; set; } = string.Empty;
    public int UsuarioId { get; set; }
    public string UsuarioNome { get; set; } = string.Empty;
    public TipoMovimento Tipo { get; set; }
    public int Quantidade { get; set; }
    public int EstoqueAnterior { get; set; }
    public int EstoqueNovo { get; set; }
    public string? Observacao { get; set; }
    public DateTime DataMovimento { get; set; }
}

public class MovimentoEstoqueCreateDto
{
    public int ProdutoId { get; set; }
    public int UsuarioId { get; set; }
    public TipoMovimento Tipo { get; set; }
    public int Quantidade { get; set; }
    public string? Observacao { get; set; }
}
