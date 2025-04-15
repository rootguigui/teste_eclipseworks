using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TesteEclipseWorks.Api.Externals;
using TesteEclipseWorks.Api.Infra;
using TesteEclipseWorks.Api.Repositories;
using TesteEclipseWorks.Api.Repositories.Interfaces;
using TesteEclipseWorks.Api.Services;
using TesteEclipseWorks.Api.Services.Interfaces;
using TesteEclipseWorks.Api.Utils;

namespace TesteEclipseWorks.Api.Configurations;

[ExcludeFromCodeCoverage]
public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDependencyInjectionExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddJsonSerializer();
        services.AddConfigureOptions(configuration);
        services.AddRepositories();
        services.AddServices();
        services.AddControllers();
        services.AddSwaggerDocs(configuration);
        return services;
    }

    public static void AddConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProjetoRepository, ProjetoRepository>();
        services.AddScoped<ITarefaRepository, TarefaRepository>();
        services.AddScoped<ITarefaHistoricoRepository, TarefaHistoricoRepository>();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProjetoService, ProjetoService>();
        services.AddScoped<ITarefaService, TarefaService>();
        services.AddScoped<IRelatorioService, RelatorioService>();
        services.AddScoped<IUsuarioExternalApiService, UsuarioExternalApiService>();
    }

    public static IServiceCollection AddJsonSerializer(this IServiceCollection services)
    {
        services.Configure<JsonOptions>(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.Converters.Add(new JsonIsoDateTimeConverter());
            options.JsonSerializerOptions.Converters.Add(new JsonIsoDateTimeOffSetConverter());
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.MaxDepth = 128;
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

        return services;
    }

    public static IServiceCollection AddSwaggerDocs(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Teste Eclipse Works API",
                Version = "v1",
                Description = "API para gerenciamento de projetos e tarefas",
                Contact = new OpenApiContact
                {
                    Name = "Equipe de Desenvolvimento",
                    Email = "contato@testeeclipseworks.com"
                }
            });
        });

        return services;
    }
}
