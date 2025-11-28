using SimplePDV.Domain.Entities;

namespace SimplePDV.Domain.Interfaces;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<Usuario?> GetByLoginAsync(string login);
    Task<bool> LoginExistsAsync(string login);
}
