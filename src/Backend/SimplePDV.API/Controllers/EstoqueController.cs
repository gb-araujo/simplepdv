using Microsoft.AspNetCore.Mvc;
using SimplePDV.Application.DTOs;
using SimplePDV.Application.Services;

namespace SimplePDV.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstoqueController : ControllerBase
{
    private readonly MovimentoEstoqueService _movimentoService;

    public EstoqueController(MovimentoEstoqueService movimentoService)
    {
        _movimentoService = movimentoService;
    }

    [HttpGet("produto/{produtoId}")]
    public async Task<ActionResult<IEnumerable<MovimentoEstoqueDto>>> GetPorProduto(int produtoId)
    {
        var movimentos = await _movimentoService.GetMovimentosPorProdutoAsync(produtoId);
        return Ok(movimentos);
    }

    [HttpGet("periodo")]
    public async Task<ActionResult<IEnumerable<MovimentoEstoqueDto>>> GetPorPeriodo(
        [FromQuery] DateTime dataInicio, 
        [FromQuery] DateTime dataFim)
    {
        var movimentos = await _movimentoService.GetMovimentosPorPeriodoAsync(dataInicio, dataFim);
        return Ok(movimentos);
    }

    [HttpPost("movimento")]
    public async Task<ActionResult<MovimentoEstoqueDto>> Create([FromBody] MovimentoEstoqueCreateDto dto)
    {
        try
        {
            var movimento = await _movimentoService.CreateAsync(dto);
            return Ok(movimento);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
