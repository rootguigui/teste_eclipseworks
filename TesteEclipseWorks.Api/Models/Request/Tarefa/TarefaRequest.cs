using TesteEclipseWorks.Api.Models.Enums;

namespace TesteEclipseWorks.Api.Models.Request.Tarefa;

public class TarefaRequest
{
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public int ProjetoId { get; set; }
    public int UsuarioId { get; set; }
    public string Comentario { get; set; } = string.Empty;
    public TarefaStatusEnum Status { get; set; }
}
