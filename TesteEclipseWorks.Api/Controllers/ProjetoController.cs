using TesteEclipseWorks.Api.Models.Response.Projeto;
using TesteEclipseWorks.Api.Services.Interfaces;
using TesteEclipseWorks.Api.Models.Request.Projeto;
using Microsoft.AspNetCore.Mvc;

namespace TesteEclipseWorks.Api.Controllers;

[ApiController] 
[Route("api/[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
public class ProjetoController : ControllerBase
{
    private readonly IProjetoService _projetoService;

    public ProjetoController(IProjetoService projetoService)
    {
        _projetoService = projetoService;
    }

    [HttpGet("obter-todos")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProjetoResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProjetoResponse>>> ObterTodosAsync()
    {
        var projetos = await _projetoService.ObterTodosAsync();
        return Ok(projetos);
    }

    [HttpGet("obter-por-id/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjetoResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProjetoResponse>> ObterPorIdAsync(int id)
    {
        var projeto = await _projetoService.ObterPorIdAsync(id);
        return Ok(projeto);
    }

    [HttpDelete("deletar/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeletarAsync(int id)
    {
        await _projetoService.DeletarAsync(id);
        return Ok();
    }

    [HttpPost("criar")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProjetoResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProjetoResponse>> CriarAsync(ProjetoRequest request)
    {
        var projeto = await _projetoService.CriarAsync(request);
        return Ok(projeto);
    }
}