using TesteEclipseWorks.Api.Models.Request.Relatorio;
using TesteEclipseWorks.Api.Models.Response.Relatorio;
using TesteEclipseWorks.Api.Services.Interfaces;
using TesteEclipseWorks.Api.Repositories.Interfaces;
using TesteEclipseWorks.Api.Externals;
namespace TesteEclipseWorks.Api.Services;

public class RelatorioService : IRelatorioService
{
    private readonly ITarefaRepository _tarefaRepository;
    private readonly IUsuarioExternalApiService _usuarioExternalApiService;

    public RelatorioService(ITarefaRepository tarefaRepository, IUsuarioExternalApiService usuarioExternalApiService)
    {
        _tarefaRepository = tarefaRepository;
        _usuarioExternalApiService = usuarioExternalApiService;
    }

    public async Task<IEnumerable<RelatorioDesempenhoResponse>> ObterRelatorioDesempenhoUltimosTrintaDias(RelatorioUltimosTrintaDiasRequest request)
    {
        // Validação do usuário
        var usuario = await _usuarioExternalApiService.ObterUsuarioPorId(request.UsuarioId) 
            ?? throw new Exception("Usuário não encontrado");
            
        if (usuario.Cargo.Nome != "Gerente") 
            throw new Exception("Usuário não tem permissão para acessar este relatório");

        // Obtenção do período para o relatório
        var (dataFim, dataInicio) = ObterDatas();
        
        // Busca das tarefas no período especificado
        var tarefas = await _tarefaRepository.GetAllByStatusAsync(
            request.Status, 
            dataInicio, 
            dataFim);

        if (!tarefas.Any())
            return Enumerable.Empty<RelatorioDesempenhoResponse>();

        // Agrupamento e transformação dos dados
        var gruposPorUsuario = tarefas.GroupBy(t => t.UsuarioId);
        var relatorios = new List<RelatorioDesempenhoResponse>();
        
        foreach (var grupo in gruposPorUsuario)
        {
            var correntUsuario = await _usuarioExternalApiService.ObterUsuarioPorId(grupo.Key);

            var relatorio = new RelatorioDesempenhoResponse
            {
                UsuarioId = grupo.Key,
                ProjetoId = grupo.First().ProjetoId,
                ProjetoNome = grupo.First().Projeto?.Nome ?? "Sem nome",
                QuantidadeTarefas = grupo.Count(),
                UsuarioNome = correntUsuario?.Nome ?? "Sem nome"
            };
            
            relatorios.Add(relatorio);
        }

        return relatorios;
    }

    private static (DateTime, DateTime) ObterDatas()
    {
        var hoje = DateTime.Today.Date;
        var trintaDiasAtras = hoje.AddDays(-30).Date;

        return (hoje, trintaDiasAtras);
    }
}
