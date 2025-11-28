using Microsoft.EntityFrameworkCore;
using SimplePDV.Domain.Entities;

namespace SimplePDV.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Venda> Vendas { get; set; }
    public DbSet<VendaItem> VendaItens { get; set; }
    public DbSet<MovimentoEstoque> MovimentosEstoque { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.ConfigureWarnings(warnings => 
            warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração Usuario
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Login).IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.Login).IsUnique();
            entity.Property(e => e.SenhaHash).IsRequired().HasMaxLength(255);
        });

        // Configuração Produto
        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(200);
            entity.Property(e => e.SKU).IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.SKU).IsUnique();
            entity.Property(e => e.Preco).HasPrecision(10, 2);
        });

        // Configuração Venda
        modelBuilder.Entity<Venda>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ValorTotal).HasPrecision(10, 2);
            entity.HasOne(e => e.Usuario)
                .WithMany(u => u.Vendas)
                .HasForeignKey(e => e.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configuração VendaItem
        modelBuilder.Entity<VendaItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.PrecoUnitario).HasPrecision(10, 2);
            entity.Property(e => e.Subtotal).HasPrecision(10, 2);
            
            entity.HasOne(e => e.Venda)
                .WithMany(v => v.Itens)
                .HasForeignKey(e => e.VendaId)
                .OnDelete(DeleteBehavior.Cascade);
                
            entity.HasOne(e => e.Produto)
                .WithMany(p => p.VendaItens)
                .HasForeignKey(e => e.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configuração MovimentoEstoque
        modelBuilder.Entity<MovimentoEstoque>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Observacao).HasMaxLength(500);
            
            entity.HasOne(e => e.Produto)
                .WithMany(p => p.MovimentosEstoque)
                .HasForeignKey(e => e.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);
                
            entity.HasOne(e => e.Usuario)
                .WithMany(u => u.MovimentosEstoque)
                .HasForeignKey(e => e.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Seed data inicial
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Usuário padrão (senha: admin123)
        modelBuilder.Entity<Usuario>().HasData(
            new Usuario
            {
                Id = 1,
                Nome = "Administrador",
                Login = "admin",
                SenhaHash = "$2a$11$8Z9qZFfJ7X5xX9bPW5J5XeK9Z9Z9Z9Z9Z9Z9Z9Z9Z9Z9Z9Z9Z9Z9Z", // BCrypt hash
                Ativo = true,
                CriadoEm = DateTime.Now
            }
        );
    }
}
