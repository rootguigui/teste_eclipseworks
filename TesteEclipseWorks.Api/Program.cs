using System.Diagnostics.CodeAnalysis;
using TesteEclipseWorks.Api.Configurations;
using TesteEclipseWorks.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDependencyInjectionExtensions(builder.Configuration);

var app = builder.Build();

app.UseMiddlewareExtensions(builder.Configuration);

app.Run();

[ExcludeFromCodeCoverage]
#pragma warning disable CA1050
public partial class Program { }
#pragma warning restore CA1050