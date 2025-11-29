using Microsoft.EntityFrameworkCore;
using SimplePDV.Domain.Entities;
using SimplePDV.WPF.Data;

namespace SimplePDV.WPF.Services;

public class VendaLocalService
{
    private readonly LocalDbContext _context;
    private readonly ProdutoLocalService _produtoService;

    public VendaLocalService(LocalDbContext context, ProdutoLocalService produtoService)
    {
        _context = context;
        _produtoService = produtoService;
    }

    public async Task<List<Venda>> GetAllAsync()
    {
        return await _context.Vendas
            .Include(v => v.Itens)
            .ThenInclude(i => i.Produto)
            .OrderByDescending(v => v.DataVenda)
            .ToListAsync();
    }

    public async Task<List<Venda>> GetNaoSincronizadasAsync()
    {
        return await _context.Vendas
            .Include(v => v.Itens)
            .ThenInclude(i => i.Produto)
            .Where(v => !v.Sincronizado)
            .ToListAsync();
    }

    public async Task<Venda> CreateAsync(Venda venda)
    {
        // Validar e atualizar estoque
        foreach (var item in venda.Itens)
        {
            var produto = await _produtoService.GetByIdAsync(item.ProdutoId);
            if (produto == null)
                throw new Exception($"Produto {item.ProdutoId} não encontrado");

            if (produto.EstoqueAtual < item.Quantidade)
                throw new Exception($"Estoque insuficiente para {produto.Nome}");

            await _produtoService.AtualizarEstoqueAsync(item.ProdutoId, item.Quantidade);
        }

        venda.Sincronizado = false;
        _context.Vendas.Add(venda);
        await _context.SaveChangesAsync();
        return venda;
    }

    public async Task MarcarComoSincronizadaAsync(int id)
    {
        var venda = await _context.Vendas.FindAsync(id);
        if (venda != null)
        {
            venda.Sincronizado = true;
            await _context.SaveChangesAsync();
        }
    }
}
