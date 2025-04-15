using Mapster;
using TesteEclipseWorks.Api.Models.Request.Projeto;
using TesteEclipseWorks.Api.Models.Response.Projeto;
using ProjetoEntity = TesteEclipseWorks.Api.Models.Entities.Projeto;
namespace TesteEclipseWorks.Api.Models.Mappers;

public static class ProjetoMapper
{
    private static TypeAdapterConfig? _configuration = null;
    private static TypeAdapterConfig? _configurationRequest = null;

    public static ProjetoResponse ToResponse(this ProjetoEntity projeto)
    {
        _configuration ??= ToMapConfig();
        return projeto.Adapt<ProjetoResponse>(_configuration!);
    }

    public static IEnumerable<ProjetoResponse> ToResponse(this IEnumerable<ProjetoEntity> projetos)
    {
        _configuration ??= ToMapConfig();
        return projetos.Adapt<IEnumerable<ProjetoResponse>>(_configuration!);
    }

    public static ProjetoEntity ToEntity(this ProjetoRequest request)
    {
        _configurationRequest ??= ToMapConfigRequest();
        return request.Adapt<ProjetoEntity>(_configurationRequest!);
    }

    public static TypeAdapterConfig? ToMapConfig()
    {
        return new TypeAdapterConfig()
            .NewConfig<ProjetoEntity, ProjetoResponse>()
            .Config;
    }

    public static TypeAdapterConfig? ToMapConfigRequest()
    {
        return new TypeAdapterConfig()
            .NewConfig<ProjetoRequest, ProjetoEntity>()
            .Config;
    }
}