using TesteEclipseWorks.Api.Infra;
using TesteEclipseWorks.Api.Models.Entities;
using TesteEclipseWorks.Api.Repositories.Interfaces;

namespace TesteEclipseWorks.Api.Repositories;

public class TarefaHistoricoRepository : BaseRepository<TarefaHistorico>, ITarefaHistoricoRepository
{
    public TarefaHistoricoRepository(AppDbContext context) : base(context) {}
}
