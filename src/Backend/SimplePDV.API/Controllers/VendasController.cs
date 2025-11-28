using Microsoft.AspNetCore.Mvc;
using SimplePDV.Application.DTOs;
using SimplePDV.Application.Services;

namespace SimplePDV.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VendasController : ControllerBase
{
    private readonly VendaService _vendaService;

    public VendasController(VendaService vendaService)
    {
        _vendaService = vendaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VendaDto>>> GetAll()
    {
        var vendas = await _vendaService.GetAllAsync();
        return Ok(vendas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VendaDto>> GetById(int id)
    {
        var venda = await _vendaService.GetByIdAsync(id);
        if (venda == null)
            return NotFound();
        return Ok(venda);
    }

    [HttpGet("periodo")]
    public async Task<ActionResult<IEnumerable<VendaDto>>> GetPorPeriodo(
        [FromQuery] DateTime dataInicio, 
        [FromQuery] DateTime dataFim)
    {
        var vendas = await _vendaService.GetVendasPorPeriodoAsync(dataInicio, dataFim);
        return Ok(vendas);
    }

    [HttpGet("nao-sincronizadas")]
    public async Task<ActionResult<IEnumerable<VendaDto>>> GetNaoSincronizadas()
    {
        var vendas = await _vendaService.GetVendasNaoSincronizadasAsync();
        return Ok(vendas);
    }

    [HttpPost]
    public async Task<ActionResult<VendaDto>> Create([FromBody] VendaCreateDto dto)
    {
        try
        {
            var venda = await _vendaService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = venda.Id }, venda);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPatch("{id}/sincronizar")]
    public async Task<ActionResult> MarcarSincronizada(int id)
    {
        await _vendaService.MarcarComoSincronizadaAsync(id);
        return NoContent();
    }
}
