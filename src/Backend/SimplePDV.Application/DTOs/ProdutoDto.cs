namespace SimplePDV.Application.DTOs;

public class ProdutoDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public int EstoqueAtual { get; set; }
    public int EstoqueMinimo { get; set; }
    public bool Ativo { get; set; }
}

public class ProdutoCreateDto
{
    public string Nome { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public int EstoqueAtual { get; set; }
    public int EstoqueMinimo { get; set; } = 0;
}

public class ProdutoUpdateDto
{
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public int EstoqueMinimo { get; set; }
    public bool Ativo { get; set; }
}
