namespace TesteEclipseWorks.Api.Models.Response.Relatorio;

public class RelatorioDesempenhoResponse
{
    public int ProjetoId { get; set; }
    public string ProjetoNome { get; set; } = string.Empty;
    public int QuantidadeTarefas { get; set; }
    public int UsuarioId { get; set; }
    public string UsuarioNome { get; set; } = string.Empty;
    
}
