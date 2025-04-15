namespace TesteEclipseWorks.Api.Models.Entities;

public class Projeto
{
    public int ProjetoId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public virtual ICollection<Tarefa> Tarefas { get; set; } = default!;
    
}
