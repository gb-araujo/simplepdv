using Microsoft.EntityFrameworkCore;
using SimplePDV.Domain.Entities;
using SimplePDV.Domain.Interfaces;
using SimplePDV.Infrastructure.Data;

namespace SimplePDV.Infrastructure.Repositories;

public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Usuario?> GetByLoginAsync(string login)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Login == login);
    }

    public async Task<bool> LoginExistsAsync(string login)
    {
        return await _dbSet.AnyAsync(u => u.Login == login);
    }
}
