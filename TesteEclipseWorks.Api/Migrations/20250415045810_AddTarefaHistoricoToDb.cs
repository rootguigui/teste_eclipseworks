using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TesteEclipseWorks.Api.Migrations
{
    [ExcludeFromCodeCoverage]
    /// <inheritdoc />
    public partial class AddTarefaHistoricoToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TarefaHistoricos_tarefas_TarefaId",
                table: "TarefaHistoricos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TarefaHistoricos",
                table: "TarefaHistoricos");

            migrationBuilder.RenameTable(
                name: "TarefaHistoricos",
                newName: "tarefa_historicos",
                newSchema: "app");

            migrationBuilder.RenameColumn(
                name: "Detalhes",
                schema: "app",
                table: "tarefa_historicos",
                newName: "detalhes");

            migrationBuilder.RenameColumn(
                name: "Data",
                schema: "app",
                table: "tarefa_historicos",
                newName: "data");

            migrationBuilder.RenameColumn(
                name: "Comentario",
                schema: "app",
                table: "tarefa_historicos",
                newName: "comentario");

            migrationBuilder.RenameColumn(
                name: "Acao",
                schema: "app",
                table: "tarefa_historicos",
                newName: "acao");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                schema: "app",
                table: "tarefa_historicos",
                newName: "usuario_id");

            migrationBuilder.RenameColumn(
                name: "TarefaId",
                schema: "app",
                table: "tarefa_historicos",
                newName: "tarefa_id");

            migrationBuilder.RenameColumn(
                name: "TarefaHistoricoId",
                schema: "app",
                table: "tarefa_historicos",
                newName: "tarefa_historico_id");

            migrationBuilder.RenameIndex(
                name: "IX_TarefaHistoricos_TarefaId",
                schema: "app",
                table: "tarefa_historicos",
                newName: "IX_tarefa_historicos_tarefa_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "data",
                schema: "app",
                table: "tarefa_historicos",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "comentario",
                schema: "app",
                table: "tarefa_historicos",
                type: "VARCHAR(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "acao",
                schema: "app",
                table: "tarefa_historicos",
                type: "INT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "usuario_id",
                schema: "app",
                table: "tarefa_historicos",
                type: "INT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "tarefa_historico_id",
                schema: "app",
                table: "tarefa_historicos",
                type: "INT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tarefa_historicos",
                schema: "app",
                table: "tarefa_historicos",
                column: "tarefa_historico_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tarefa_historicos_tarefas_tarefa_id",
                schema: "app",
                table: "tarefa_historicos",
                column: "tarefa_id",
                principalSchema: "app",
                principalTable: "tarefas",
                principalColumn: "tarefa_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tarefa_historicos_tarefas_tarefa_id",
                schema: "app",
                table: "tarefa_historicos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tarefa_historicos",
                schema: "app",
                table: "tarefa_historicos");

            migrationBuilder.RenameTable(
                name: "tarefa_historicos",
                schema: "app",
                newName: "TarefaHistoricos");

            migrationBuilder.RenameColumn(
                name: "detalhes",
                table: "TarefaHistoricos",
                newName: "Detalhes");

            migrationBuilder.RenameColumn(
                name: "data",
                table: "TarefaHistoricos",
                newName: "Data");

            migrationBuilder.RenameColumn(
                name: "comentario",
                table: "TarefaHistoricos",
                newName: "Comentario");

            migrationBuilder.RenameColumn(
                name: "acao",
                table: "TarefaHistoricos",
                newName: "Acao");

            migrationBuilder.RenameColumn(
                name: "usuario_id",
                table: "TarefaHistoricos",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "tarefa_id",
                table: "TarefaHistoricos",
                newName: "TarefaId");

            migrationBuilder.RenameColumn(
                name: "tarefa_historico_id",
                table: "TarefaHistoricos",
                newName: "TarefaHistoricoId");

            migrationBuilder.RenameIndex(
                name: "IX_tarefa_historicos_tarefa_id",
                table: "TarefaHistoricos",
                newName: "IX_TarefaHistoricos_TarefaId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data",
                table: "TarefaHistoricos",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<string>(
                name: "Comentario",
                table: "TarefaHistoricos",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(255)");

            migrationBuilder.AlterColumn<int>(
                name: "Acao",
                table: "TarefaHistoricos",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "TarefaHistoricos",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AlterColumn<int>(
                name: "TarefaHistoricoId",
                table: "TarefaHistoricos",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TarefaHistoricos",
                table: "TarefaHistoricos",
                column: "TarefaHistoricoId");

            migrationBuilder.AddForeignKey(
                name: "FK_TarefaHistoricos_tarefas_TarefaId",
                table: "TarefaHistoricos",
                column: "TarefaId",
                principalSchema: "app",
                principalTable: "tarefas",
                principalColumn: "tarefa_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
