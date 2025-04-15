namespace TesteEclipseWorks.Api.Models.Response.Tarefa;

public class TarefaResponse
{
    public int TarefaId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public int ProjetoId { get; set; }
    public string ProjetoNome { get; set; } = string.Empty;
}
