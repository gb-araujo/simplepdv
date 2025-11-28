namespace SimplePDV.Domain.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.Now;
    public DateTime? AtualizadoEm { get; set; }
}
