using Microsoft.AspNetCore.Mvc;
using TesteEclipseWorks.Api.Models.Request.Relatorio;
using TesteEclipseWorks.Api.Models.Response.Relatorio;
using TesteEclipseWorks.Api.Services.Interfaces;

namespace TesteEclipseWorks.Api.Controllers;

[ApiController] 
[Route("api/[controller]")]
public class RelatorioController : ControllerBase
{
    private readonly IRelatorioService _relatorioService;

    public RelatorioController(IRelatorioService relatorioService)
    {
        _relatorioService = relatorioService;
    }

    [HttpGet("relatorio-desempenho-ultimos-trinta-dias")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<RelatorioDesempenhoResponse>>> ObterRelatorioDesempenhoUltimosTrintaDias
    (
        [FromQuery] RelatorioUltimosTrintaDiasRequest request
    )
    {
        var relatorio = await _relatorioService.ObterRelatorioDesempenhoUltimosTrintaDias(request);

        return Ok(relatorio);
    }
}
