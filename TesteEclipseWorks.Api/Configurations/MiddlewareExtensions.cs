using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using TesteEclipseWorks.Api.Infra;

namespace TesteEclipseWorks.Api.Configurations;

[ExcludeFromCodeCoverage]
public static class MiddlewareExtensions
{
    public static void UseMiddlewareExtensions(this IApplicationBuilder app, IConfiguration configuration)
    {
        if (app == null) throw new ArgumentNullException(nameof(app));

        // Aplicar migrações automaticamente
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<AppDbContext>();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<object>>();
                logger.LogError(ex, "Ocorreu um erro ao aplicar as migrações do banco de dados.");
            }
        }

        app.UseStaticFiles();
        app.UseCors(options =>
        {
            options.AllowAnyHeader();
            options.AllowAnyMethod();
            options.AllowAnyOrigin();
        });
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        // Configuração do Swagger
        app.UseSwagger(c =>
        {
            c.RouteTemplate = "swagger/{documentName}/swagger.json";
        });
        
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Teste Eclipse Works API v1");
            c.RoutePrefix = "swagger";
            c.DocumentTitle = "Teste Eclipse Works API Documentation";
            c.DefaultModelsExpandDepth(-1); // Oculta os schemas por padrão
            c.DisplayRequestDuration();
            c.EnableDeepLinking();
            c.EnableFilter();
        });
    }
}
