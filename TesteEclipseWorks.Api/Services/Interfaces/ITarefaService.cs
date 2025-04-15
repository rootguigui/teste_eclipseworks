using TesteEclipseWorks.Api.Models.Request.Tarefa;
using TesteEclipseWorks.Api.Models.Response.Tarefa;

namespace TesteEclipseWorks.Api.Services.Interfaces;

public interface ITarefaService
{
    Task<TarefaResponse> CriarAsync(TarefaRequest request);
    Task<TarefaResponse> ObterPorIdAsync(int tarefaId);
    Task<IEnumerable<TarefaResponse>> ObterTodosPorProjetoIdAsync(int projetoId);
    Task<bool> DeletarAsync(int tarefaId, string comentario);
    Task<TarefaResponse> AtualizarAsync(int tarefaId, TarefaRequest request);
}
