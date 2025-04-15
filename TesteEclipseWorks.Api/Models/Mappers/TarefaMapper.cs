using Mapster;
using TesteEclipseWorks.Api.Models.Request.Tarefa;
using TesteEclipseWorks.Api.Models.Response.Tarefa;
using TarefaEntity = TesteEclipseWorks.Api.Models.Entities.Tarefa;

namespace TesteEclipseWorks.Api.Models.Mappers;

public static class TarefaMapper
{
    private static TypeAdapterConfig? _configuration = null;
    private static TypeAdapterConfig? _configurationRequest = null;

    public static TarefaResponse ToResponse(this TarefaEntity tarefa)
    {
        _configuration ??= ToMapConfig();
        
        return tarefa.Adapt<TarefaResponse>(_configuration!);
    }

    public static IEnumerable<TarefaResponse> ToResponse(this IEnumerable<TarefaEntity> tarefas)
    {
        _configuration ??= ToMapConfig();
        return tarefas.Adapt<IEnumerable<TarefaResponse>>(_configuration!);
    }

    public static TarefaEntity ToEntity(this TarefaRequest request)
    {
        _configurationRequest ??= ToMapConfigRequest();
        return request.Adapt<TarefaEntity>(_configurationRequest!);
    }

    public static TypeAdapterConfig? ToMapConfig()
    {
        return new TypeAdapterConfig()
            .NewConfig<TarefaEntity, TarefaResponse>()
            .Config;
    }

    public static TypeAdapterConfig? ToMapConfigRequest()
    {
        return new TypeAdapterConfig()
            .NewConfig<TarefaRequest, TarefaEntity>()
            .Config;
    }
}
