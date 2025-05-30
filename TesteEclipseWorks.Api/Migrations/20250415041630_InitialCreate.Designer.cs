﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TesteEclipseWorks.Api.Infra;

#nullable disable

namespace TesteEclipseWorks.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250415041630_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TesteEclipseWorks.Api.Models.Entities.Projeto", b =>
                {
                    b.Property<int>("ProjetoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasColumnName("projeto_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProjetoId"));

                    b.Property<DateTime>("DataFim")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("data_fim");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("data_inicio");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("descricao");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("nome");

                    b.HasKey("ProjetoId");

                    b.ToTable("projetos", "app");
                });

            modelBuilder.Entity("TesteEclipseWorks.Api.Models.Entities.Tarefa", b =>
                {
                    b.Property<int>("TarefaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasColumnName("tarefa_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TarefaId"));

                    b.Property<DateTime?>("DataFim")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("data_fim");

                    b.Property<DateTime?>("DataInicio")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("data_inicio");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("descricao");

                    b.Property<int>("Prioridade")
                        .HasColumnType("integer");

                    b.Property<int>("ProjetoId")
                        .HasColumnType("INT")
                        .HasColumnName("projeto_id");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("titulo");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("integer");

                    b.HasKey("TarefaId");

                    b.HasIndex("ProjetoId");

                    b.ToTable("tarefas", "app");
                });

            modelBuilder.Entity("TesteEclipseWorks.Api.Models.Entities.TarefaHistorico", b =>
                {
                    b.Property<int>("TarefaHistoricoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TarefaHistoricoId"));

                    b.Property<int>("Acao")
                        .HasColumnType("integer");

                    b.Property<string>("Comentario")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Data")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Detalhes")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TarefaId")
                        .HasColumnType("INT");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("integer");

                    b.HasKey("TarefaHistoricoId");

                    b.HasIndex("TarefaId");

                    b.ToTable("TarefaHistorico");
                });

            modelBuilder.Entity("TesteEclipseWorks.Api.Models.Entities.Tarefa", b =>
                {
                    b.HasOne("TesteEclipseWorks.Api.Models.Entities.Projeto", "Projeto")
                        .WithMany("Tarefas")
                        .HasForeignKey("ProjetoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Projeto");
                });

            modelBuilder.Entity("TesteEclipseWorks.Api.Models.Entities.TarefaHistorico", b =>
                {
                    b.HasOne("TesteEclipseWorks.Api.Models.Entities.Tarefa", "Tarefa")
                        .WithMany("Historico")
                        .HasForeignKey("TarefaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tarefa");
                });

            modelBuilder.Entity("TesteEclipseWorks.Api.Models.Entities.Projeto", b =>
                {
                    b.Navigation("Tarefas");
                });

            modelBuilder.Entity("TesteEclipseWorks.Api.Models.Entities.Tarefa", b =>
                {
                    b.Navigation("Historico");
                });
#pragma warning restore 612, 618
        }
    }
}
