using SimplePDV.Domain.Enums;

namespace SimplePDV.Application.DTOs;

public class VendaDto
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public string UsuarioNome { get; set; } = string.Empty;
    public DateTime DataVenda { get; set; }
    public decimal ValorTotal { get; set; }
    public FormaPagamento FormaPagamento { get; set; }
    public List<VendaItemDto> Itens { get; set; } = new();
}

public class VendaCreateDto
{
    public int UsuarioId { get; set; }
    public FormaPagamento FormaPagamento { get; set; }
    public List<VendaItemCreateDto> Itens { get; set; } = new();
}

public class VendaItemDto
{
    public int Id { get; set; }
    public int ProdutoId { get; set; }
    public string ProdutoNome { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public decimal Subtotal { get; set; }
}

public class VendaItemCreateDto
{
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }
}
