using TesteEclipseWorks.Api.Models.Mappers;
using TesteEclipseWorks.Api.Models.Request.Projeto;
using TesteEclipseWorks.Api.Models.Response.Projeto;
using TesteEclipseWorks.Api.Repositories.Interfaces;
using TesteEclipseWorks.Api.Services.Interfaces;

namespace TesteEclipseWorks.Api.Services;

public class ProjetoService : IProjetoService
{
    private readonly IProjetoRepository _projetoRepository;
    private readonly ITarefaRepository _tarefaRepository;

    public ProjetoService(IProjetoRepository projetoRepository, ITarefaRepository tarefaRepository)
    {
        _projetoRepository = projetoRepository;
        _tarefaRepository = tarefaRepository;
    }

    public async Task<bool> DeletarAsync(int id)
    {
        var projeto = await _projetoRepository.GetByIdAsync(id);
        if (projeto == null) throw new Exception("Projeto não encontrado");

        var temTarefasPendentes = await _tarefaRepository.TemTarefasPendentesAsync(id);
        if (temTarefasPendentes) throw new Exception("Projeto possui tarefas pendentes, conclua as tarefas pendentes para deletar o projeto");

        await _projetoRepository.DeleteAsync(projeto.ProjetoId);
        return true;
    }

    public async Task<ProjetoResponse> ObterPorIdAsync(int id)
    {
        var projeto = await _projetoRepository.GetByIdAsync(id);

        if (projeto == null) throw new Exception("Projeto não encontrado");

        return projeto.ToResponse();
    }

    public async Task<IEnumerable<ProjetoResponse>> ObterTodosAsync()
    {
        var projetos = await _projetoRepository.GetAllAsync();

        return projetos.ToResponse();
    }

    public async Task<ProjetoResponse> CriarAsync(ProjetoRequest request)
    {
        var projeto = request.ToEntity();
        await _projetoRepository.AddAsync(projeto);
        return projeto.ToResponse();
    }

}
