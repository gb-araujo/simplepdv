using Microsoft.AspNetCore.Mvc;
using SimplePDV.Application.DTOs;
using SimplePDV.Application.Services;

namespace SimplePDV.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly UsuarioService _usuarioService;

    public UsuariosController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetAll()
    {
        var usuarios = await _usuarioService.GetAllAsync();
        return Ok(usuarios);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UsuarioDto>> GetById(int id)
    {
        var usuario = await _usuarioService.GetByIdAsync(id);
        if (usuario == null)
            return NotFound();
        return Ok(usuario);
    }

    [HttpPost]
    public async Task<ActionResult<UsuarioDto>> Create([FromBody] UsuarioCreateDto dto)
    {
        try
        {
            var usuario = await _usuarioService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginDto dto)
    {
        var result = await _usuarioService.LoginAsync(dto);
        if (result == null)
            return Unauthorized(new { message = "Login ou senha inválidos" });
        return Ok(result);
    }

    [HttpPut("{id}/senha")]
    public async Task<ActionResult> AlterarSenha(int id, [FromBody] string novaSenha)
    {
        try
        {
            await _usuarioService.AlterarSenhaAsync(id, novaSenha);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
