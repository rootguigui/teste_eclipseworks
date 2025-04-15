using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteEclipseWorks.Api.Models.Entities;
using System.Diagnostics.CodeAnalysis;

namespace TesteEclipseWorks.Api.Infra.Mapping;

[ExcludeFromCodeCoverage]
public class TarefaMap : IEntityTypeConfiguration<Tarefa>
{
    public void Configure(EntityTypeBuilder<Tarefa> builder)
    {
        builder.ToTable("tarefas", "app");
        builder.Property(t => t.TarefaId).UseIdentityColumn().HasColumnName("tarefa_id").HasColumnType("INT");
        builder.Property(t => t.Titulo).HasColumnName("titulo").HasMaxLength(150).IsRequired();
        builder.Property(t => t.Descricao).HasColumnName("descricao").HasMaxLength(200).IsRequired();
        builder.Property(t => t.DataInicio).HasColumnName("data_inicio").HasColumnType("timestamp").IsRequired(false);
        builder.Property(t => t.DataFim).HasColumnName("data_fim").HasColumnType("timestamp").IsRequired(false);
        builder.Property(t => t.Status).HasColumnName("status").IsRequired();
        builder.Property(t => t.ProjetoId).HasColumnName("projeto_id").HasColumnType("INT");
        builder.Property(t => t.UsuarioId).HasColumnName("usuario_id").HasColumnType("INT").IsRequired();
        builder.Property(t => t.Prioridade).HasColumnName("prioridade").IsRequired();

        builder.HasOne(t => t.Projeto).WithMany(p => p.Tarefas);
    }
}
