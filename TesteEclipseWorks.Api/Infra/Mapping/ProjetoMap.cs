using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteEclipseWorks.Api.Models.Entities;
using System.Diagnostics.CodeAnalysis;
namespace TesteEclipseWorks.Api.Infra.Mapping;

[ExcludeFromCodeCoverage]
public class ProjetoMap : IEntityTypeConfiguration<Projeto>
{
    public void Configure(EntityTypeBuilder<Projeto> builder)
    {
        builder.ToTable("projetos", "app");
        builder.Property(p => p.ProjetoId).UseIdentityColumn().HasColumnName("projeto_id").HasColumnType("INT");
        builder.Property(p => p.Nome).HasColumnName("nome").HasMaxLength(150).IsRequired();
        builder.Property(p => p.Descricao).HasColumnName("descricao").HasMaxLength(200).IsRequired();
        builder.Property(p => p.DataInicio).HasColumnName("data_inicio").HasColumnType("timestamp").IsRequired();
        builder.Property(p => p.DataFim).HasColumnName("data_fim").HasColumnType("timestamp").IsRequired();

        builder.HasMany(p => p.Tarefas).WithOne(t => t.Projeto);
    }
}
