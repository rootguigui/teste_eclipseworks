using TesteEclipseWorks.Api.Models.Enums;

namespace TesteEclipseWorks.Api.Models.Entities;

public class Tarefa
{
    public int TarefaId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public int ProjetoId { get; set; }
    public Projeto Projeto { get; set; } = default!;
    public int UsuarioId { get; set; }
    public TarefaStatusEnum Status { get; set; } = TarefaStatusEnum.Pendente;
    public TarefaPrioridadeEnum Prioridade { get; set; } = TarefaPrioridadeEnum.Baixa;
    public virtual ICollection<TarefaHistorico> Historico { get; set; } = default!;
}
