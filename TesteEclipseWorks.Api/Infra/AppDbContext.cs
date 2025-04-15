using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using TesteEclipseWorks.Api.Infra.Mapping;
using TesteEclipseWorks.Api.Models.Entities;

namespace TesteEclipseWorks.Api.Infra;

[ExcludeFromCodeCoverage]
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Projeto> Projetos { get; set; }
    public DbSet<Tarefa> Tarefas { get; set; }
    public DbSet<TarefaHistorico> TarefaHistoricos { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProjetoMap());
        modelBuilder.ApplyConfiguration(new TarefaMap());
        modelBuilder.ApplyConfiguration(new TarefaHistoricoMap());
        base.OnModelCreating(modelBuilder);
    }
}
