using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteEclipseWorks.Api.Models.Entities;
using System.Diagnostics.CodeAnalysis;

namespace TesteEclipseWorks.Api.Infra.Mapping;

[ExcludeFromCodeCoverage]
public class TarefaHistoricoMap : IEntityTypeConfiguration<TarefaHistorico>
{
    public void Configure(EntityTypeBuilder<TarefaHistorico> builder)
    {
        builder.ToTable("tarefa_historicos", "app");
        builder.Property(t => t.TarefaHistoricoId).UseIdentityColumn().HasColumnName("tarefa_historico_id").HasColumnType("INT");
        builder.Property(t => t.TarefaId).HasColumnName("tarefa_id").HasColumnType("INT");
        builder.Property(t => t.UsuarioId).HasColumnName("usuario_id").HasColumnType("INT");
        builder.Property(t => t.Data).HasColumnName("data").HasColumnType("timestamp");
        builder.Property(t => t.Acao).HasColumnName("acao").HasColumnType("INT");
        builder.Property(t => t.Comentario).HasColumnName("comentario").HasColumnType("VARCHAR(255)");
        builder.Property(t => t.Detalhes).HasColumnName("detalhes").HasColumnType("text");
    }
}
