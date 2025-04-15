using TesteEclipseWorks.Api.Models.Enums;

namespace TesteEclipseWorks.Api.Models.Request.Relatorio;

public class RelatorioUltimosTrintaDiasRequest
{
    public TarefaStatusEnum Status { get; set; }
    public int UsuarioId { get; set; }
}
