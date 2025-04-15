using TesteEclipseWorks.Api.Models.Request.Projeto;
using TesteEclipseWorks.Api.Models.Response.Projeto;
namespace TesteEclipseWorks.Api.Services.Interfaces;

public interface IProjetoService
{
    Task<IEnumerable<ProjetoResponse>> ObterTodosAsync();
    Task<ProjetoResponse> ObterPorIdAsync(int id);
    Task<bool> DeletarAsync(int id);
    Task<ProjetoResponse> CriarAsync(ProjetoRequest request);
}
