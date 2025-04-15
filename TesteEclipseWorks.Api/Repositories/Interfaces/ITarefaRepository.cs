using TesteEclipseWorks.Api.Models.Entities;
using TesteEclipseWorks.Api.Models.Enums;

namespace TesteEclipseWorks.Api.Repositories.Interfaces;

public interface ITarefaRepository : IBaseRepository<Tarefa>
{
    Task<IEnumerable<Tarefa>> GetAllByProjetoIdAsync(int projetoId);
    Task<bool> TemTarefasPendentesAsync(int projetoId);
    Task<IEnumerable<Tarefa>> GetAllByStatusAsync(TarefaStatusEnum status, DateTime dataInicio, DateTime dataFim);
}
