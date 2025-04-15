using TesteEclipseWorks.Api.Infra;
using TesteEclipseWorks.Api.Models.Entities;
using TesteEclipseWorks.Api.Repositories.Interfaces;

namespace TesteEclipseWorks.Api.Repositories;

public class ProjetoRepository : BaseRepository<Projeto>, IProjetoRepository
{
    public ProjetoRepository(AppDbContext context) : base(context)
    {
    }
}
