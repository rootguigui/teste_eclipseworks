using TesteEclipseWorks.Api.Models.Enums;

namespace TesteEclipseWorks.Api.Models.Entities;

public class TarefaHistorico
{
    public int TarefaHistoricoId { get; set; }
    public int TarefaId { get; set; }
    public TarefaHistoricoAcaoEnum Acao { get; set; }
    public int UsuarioId { get; set; }
    public DateTime Data { get; set; }
    public string Comentario { get; set; } = string.Empty;
    public string Detalhes { get; set; } = string.Empty;

    public virtual Tarefa Tarefa { get; set; } = default!;
}
