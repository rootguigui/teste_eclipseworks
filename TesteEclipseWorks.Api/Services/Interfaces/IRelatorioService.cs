using TesteEclipseWorks.Api.Models.Request.Relatorio;
using TesteEclipseWorks.Api.Models.Response.Relatorio;

namespace TesteEclipseWorks.Api.Services.Interfaces;

public interface IRelatorioService
{
    Task<IEnumerable<RelatorioDesempenhoResponse>> ObterRelatorioDesempenhoUltimosTrintaDias
    (
        RelatorioUltimosTrintaDiasRequest request
    );
}
