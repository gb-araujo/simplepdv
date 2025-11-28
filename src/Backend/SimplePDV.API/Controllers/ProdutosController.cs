using Microsoft.AspNetCore.Mvc;
using SimplePDV.Application.DTOs;
using SimplePDV.Application.Services;

namespace SimplePDV.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly ProdutoService _produtoService;

    public ProdutosController(ProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetAll()
    {
        var produtos = await _produtoService.GetAllAsync();
        return Ok(produtos);
    }

    [HttpGet("ativos")]
    public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetAtivos()
    {
        var produtos = await _produtoService.GetAtivosAsync();
        return Ok(produtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProdutoDto>> GetById(int id)
    {
        var produto = await _produtoService.GetByIdAsync(id);
        if (produto == null)
            return NotFound();
        return Ok(produto);
    }

    [HttpGet("sku/{sku}")]
    public async Task<ActionResult<ProdutoDto>> GetBySKU(string sku)
    {
        var produto = await _produtoService.GetBySKUAsync(sku);
        if (produto == null)
            return NotFound();
        return Ok(produto);
    }

    [HttpGet("estoque-baixo")]
    public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetEstoqueBaixo()
    {
        var produtos = await _produtoService.GetProdutosEstoqueBaixoAsync();
        return Ok(produtos);
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoDto>> Create([FromBody] ProdutoCreateDto dto)
    {
        try
        {
            var produto = await _produtoService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProdutoDto>> Update(int id, [FromBody] ProdutoUpdateDto dto)
    {
        try
        {
            var produto = await _produtoService.UpdateAsync(id, dto);
            return Ok(produto);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _produtoService.DeleteAsync(id);
        return NoContent();
    }
}
