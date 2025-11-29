using Microsoft.EntityFrameworkCore;
using SimplePDV.Domain.Entities;
using SimplePDV.WPF.Data;

namespace SimplePDV.WPF.Services;

public class UsuarioLocalService
{
    private readonly LocalDbContext _context;

    public UsuarioLocalService(LocalDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> LoginAsync(string login, string senha)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Login == login);
        if (usuario == null || !usuario.Ativo)
            return null;

        // Verificar senha (BCrypt)
        if (!BCrypt.Net.BCrypt.Verify(senha, usuario.SenhaHash))
            return null;

        return usuario;
    }

    public async Task AddOrUpdateAsync(Usuario usuario)
    {
        var existing = await _context.Usuarios.FindAsync(usuario.Id);
        if (existing != null)
        {
            _context.Entry(existing).CurrentValues.SetValues(usuario);
        }
        else
        {
            _context.Usuarios.Add(usuario);
        }
        await _context.SaveChangesAsync();
    }
}
