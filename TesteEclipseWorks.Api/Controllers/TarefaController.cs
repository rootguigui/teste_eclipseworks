using Microsoft.AspNetCore.Mvc;
using TesteEclipseWorks.Api.Models.Request.Tarefa;
using TesteEclipseWorks.Api.Models.Response.Tarefa;
using TesteEclipseWorks.Api.Services.Interfaces;
namespace TesteEclipseWorks.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TarefaController : ControllerBase
{
    private readonly ITarefaService _tarefaService;

    public TarefaController(ITarefaService tarefaService)
    {
        _tarefaService = tarefaService;
    }

    [HttpGet("obter-por-id/{tarefaId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TarefaResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TarefaResponse>> ObterPorIdAsync([FromRoute] int tarefaId)
    {
        var tarefa = await _tarefaService.ObterPorIdAsync(tarefaId);
        return Ok(tarefa);
    }

    [HttpGet("obter-todos-por-projeto/{projetoId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TarefaResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TarefaResponse>>> ObterTodosPorProjetoIdAsync([FromRoute] int projetoId)
    {
        var tarefas = await _tarefaService.ObterTodosPorProjetoIdAsync(projetoId);
        return Ok(tarefas);
    }

    [HttpPost("criar")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TarefaResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TarefaResponse>> CriarAsync([FromBody] TarefaRequest request)
    {
        var tarefa = await _tarefaService.CriarAsync(request);
        return Ok(tarefa);
    }

    [HttpPut("atualizar/{tarefaId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TarefaResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TarefaResponse>> AtualizarAsync([FromRoute] int tarefaId, [FromBody] TarefaRequest request)
    {
        var tarefa = await _tarefaService.AtualizarAsync(tarefaId, request);
        return Ok(tarefa);
    }
    
    [HttpDelete("deletar/{tarefaId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletarAsync([FromRoute] int tarefaId, [FromQuery] string comentario)
    {
        var result = await _tarefaService.DeletarAsync(tarefaId, comentario);
        return result ? NoContent() : BadRequest();
    }
}
