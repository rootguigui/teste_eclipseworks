using Microsoft.EntityFrameworkCore;
using TesteEclipseWorks.Api.Infra;
using TesteEclipseWorks.Api.Models.Entities;
using TesteEclipseWorks.Api.Models.Enums;
using TesteEclipseWorks.Api.Repositories.Interfaces;

namespace TesteEclipseWorks.Api.Repositories;

public class TarefaRepository : BaseRepository<Tarefa>, ITarefaRepository
{
    public TarefaRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Tarefa>> GetAllByProjetoIdAsync(int projetoId)
    {
        return await _context.Tarefas.Where(t => t.ProjetoId == projetoId).ToListAsync();
    }

    public async Task<bool> TemTarefasPendentesAsync(int projetoId)
    {
        return await _context.Tarefas.AnyAsync(t => t.ProjetoId == projetoId && t.Status == TarefaStatusEnum.Pendente);
    }

    public async Task<IEnumerable<Tarefa>> GetAllByStatusAsync(TarefaStatusEnum status, DateTime dataInicio, DateTime dataFim)
    {
        return await _context.Tarefas.Include(t => t.Projeto).Where(t => t.Status == status && 
            (!t.DataInicio.HasValue || t.DataInicio.Value.Date >= dataInicio.Date) && 
            (!t.DataFim.HasValue || t.DataFim.Value.Date <= dataFim.Date)).ToListAsync();
    }
}
