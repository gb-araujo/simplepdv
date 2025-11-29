using Microsoft.EntityFrameworkCore;
using SimplePDV.Domain.Entities;
using System.IO;

namespace SimplePDV.WPF.Data;

public class LocalDbContext : DbContext
{
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Venda> Vendas { get; set; }
    public DbSet<VendaItem> VendaItens { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "SimplePDV",
            "simplepdv.db");

        Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired();
            entity.Property(e => e.Login).IsRequired();
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired();
            entity.Property(e => e.SKU).IsRequired();
        });

        modelBuilder.Entity<Venda>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Usuario)
                .WithMany(u => u.Vendas)
                .HasForeignKey(e => e.UsuarioId);
        });

        modelBuilder.Entity<VendaItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Venda)
                .WithMany(v => v.Itens)
                .HasForeignKey(e => e.VendaId);
            entity.HasOne(e => e.Produto)
                .WithMany(p => p.VendaItens)
                .HasForeignKey(e => e.ProdutoId);
        });
    }
}
