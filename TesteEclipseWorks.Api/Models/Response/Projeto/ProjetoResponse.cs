namespace TesteEclipseWorks.Api.Models.Response.Projeto;

public class ProjetoResponse
{
    public int ProjetoId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
}
