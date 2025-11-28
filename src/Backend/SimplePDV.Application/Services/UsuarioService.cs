using BCrypt.Net;
using SimplePDV.Application.DTOs;
using SimplePDV.Domain.Entities;
using SimplePDV.Domain.Interfaces;

namespace SimplePDV.Application.Services;

public class UsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
    {
        var usuarios = await _usuarioRepository.GetAllAsync();
        return usuarios.Select(MapToDto);
    }

    public async Task<UsuarioDto?> GetByIdAsync(int id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        return usuario != null ? MapToDto(usuario) : null;
    }

    public async Task<UsuarioDto> CreateAsync(UsuarioCreateDto dto)
    {
        if (await _usuarioRepository.LoginExistsAsync(dto.Login))
            throw new Exception("Login já existe");

        var usuario = new Usuario
        {
            Nome = dto.Nome,
            Login = dto.Login,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha),
            Ativo = true
        };

        await _usuarioRepository.AddAsync(usuario);
        return MapToDto(usuario);
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginDto dto)
    {
        var usuario = await _usuarioRepository.GetByLoginAsync(dto.Login);
        
        if (usuario == null || !usuario.Ativo)
            return null;

        if (!BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.SenhaHash))
            return null;

        // Token simples - em produção use JWT
        var token = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{usuario.Login}:{DateTime.Now.Ticks}"));

        return new LoginResponseDto
        {
            Token = token,
            Usuario = MapToDto(usuario)
        };
    }

    public async Task AlterarSenhaAsync(int id, string novaSenha)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario == null)
            throw new Exception("Usuário não encontrado");

        usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(novaSenha);
        await _usuarioRepository.UpdateAsync(usuario);
    }

    private static UsuarioDto MapToDto(Usuario usuario)
    {
        return new UsuarioDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Login = usuario.Login,
            Ativo = usuario.Ativo
        };
    }
}
