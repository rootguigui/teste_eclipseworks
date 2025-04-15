using System.Text;
using System.Text.Json;
using TesteEclipseWorks.Api.Models.Entities;
using TesteEclipseWorks.Api.Models.Enums;
using TesteEclipseWorks.Api.Models.Mappers;
using TesteEclipseWorks.Api.Models.Request.Tarefa;
using TesteEclipseWorks.Api.Models.Response.Tarefa;
using TesteEclipseWorks.Api.Repositories.Interfaces;
using TesteEclipseWorks.Api.Services.Interfaces;

namespace TesteEclipseWorks.Api.Services;

public class TarefaService : ITarefaService
{
    private readonly ITarefaRepository _tarefaRepository;
    private readonly ITarefaHistoricoRepository _tarefaHistoricoRepository;

    public TarefaService(ITarefaRepository tarefaRepository, ITarefaHistoricoRepository tarefaHistoricoRepository)
    {
        _tarefaRepository = tarefaRepository;
        _tarefaHistoricoRepository = tarefaHistoricoRepository;
    }

    public async Task<TarefaResponse> AtualizarAsync(int tarefaId, TarefaRequest request)
    {
        var tarefa = await _tarefaRepository.GetByIdAsync(tarefaId);
        if (tarefa == null) throw new Exception("Tarefa não encontrada");

        Console.WriteLine(JsonSerializer.Serialize(tarefa));

        var tarefaHistorico = new TarefaHistorico
        {
            TarefaId = tarefa.TarefaId,
            Acao = TarefaHistoricoAcaoEnum.Atualizacao,
            UsuarioId = tarefa.UsuarioId,
            Data = DateTime.Now,
            Comentario = request.Comentario,
            Detalhes = ComparacaoTarefas(tarefa, request)
        };

        await _tarefaHistoricoRepository.AddAsync(tarefaHistorico);

        tarefa.Titulo = request.Titulo;
        tarefa.Descricao = request.Descricao;
        tarefa.DataInicio = request.DataInicio;
        tarefa.DataFim = request.DataFim;
        tarefa.Status = request.Status;
        
        await _tarefaRepository.UpdateAsync(tarefa);

        return tarefa.ToResponse();
    }

    public async Task<TarefaResponse> CriarAsync(TarefaRequest request)
    {
        var tarefa = request.ToEntity();

        var tarefasProjetos = await _tarefaRepository.GetAllByProjetoIdAsync(request.ProjetoId);
        if (tarefasProjetos.Count() >= 20) throw new Exception("Projeto possui o máximo de tarefas");

        await _tarefaRepository.AddAsync(tarefa);
        return tarefa.ToResponse();
    }

    public async Task<bool> DeletarAsync(int tarefaId, string comentario)
    {
        var tarefa = await _tarefaRepository.GetByIdAsync(tarefaId);
        if (tarefa == null) throw new Exception("Tarefa não encontrada");

        var tarefaHistorico = new TarefaHistorico
        {
            TarefaId = tarefaId,
            Acao = TarefaHistoricoAcaoEnum.Exclusao,
            UsuarioId = tarefa.UsuarioId,
            Data = DateTime.Now,
            Comentario = comentario,
        };

        await _tarefaHistoricoRepository.AddAsync(tarefaHistorico);

        return await _tarefaRepository.DeleteAsync(tarefaId);
    }

    public async Task<TarefaResponse> ObterPorIdAsync(int tarefaId)
    {
        var tarefa = await _tarefaRepository.GetByIdAsync(tarefaId);
        return tarefa!.ToResponse();
    }

    public async Task<IEnumerable<TarefaResponse>> ObterTodosPorProjetoIdAsync(int projetoId)
    {
        var tarefas = await _tarefaRepository.GetAllByProjetoIdAsync(projetoId);
        return tarefas.ToResponse();
    }

    private static string ComparacaoTarefas(Tarefa tarefa, TarefaRequest request)
    {
        var detalhes = new StringBuilder();

        if (tarefa.Titulo != request.Titulo)
            detalhes.AppendLine($"Título alterado de {tarefa.Titulo} para {request.Titulo}");
        if (tarefa.Descricao != request.Descricao)
            detalhes.AppendLine($"Descrição alterada de {tarefa.Descricao} para {request.Descricao}");
        if (tarefa.DataInicio != request.DataInicio)
            detalhes.AppendLine($"Data de início alterada de {tarefa.DataInicio} para {request.DataInicio}");
        if (tarefa.DataFim != request.DataFim)
            detalhes.AppendLine($"Data de término alterada de {tarefa.DataFim} para {request.DataFim}");

        return detalhes.ToString();
    }
}
