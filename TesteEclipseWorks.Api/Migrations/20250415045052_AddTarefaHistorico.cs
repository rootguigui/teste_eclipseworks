using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace TesteEclipseWorks.Api.Migrations
{
    [ExcludeFromCodeCoverage]
    /// <inheritdoc />
    public partial class AddTarefaHistorico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TarefaHistorico_tarefas_TarefaId",
                table: "TarefaHistorico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TarefaHistorico",
                table: "TarefaHistorico");

            migrationBuilder.RenameTable(
                name: "TarefaHistorico",
                newName: "TarefaHistoricos");

            migrationBuilder.RenameIndex(
                name: "IX_TarefaHistorico_TarefaId",
                table: "TarefaHistoricos",
                newName: "IX_TarefaHistoricos_TarefaId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TarefaHistoricos_tarefas_TarefaId",
                table: "TarefaHistoricos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TarefaHistoricos",
                table: "TarefaHistoricos");

            migrationBuilder.RenameTable(
                name: "TarefaHistoricos",
                newName: "TarefaHistorico");

            migrationBuilder.RenameIndex(
                name: "IX_TarefaHistoricos_TarefaId",
                table: "TarefaHistorico",
                newName: "IX_TarefaHistorico_TarefaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TarefaHistorico",
                table: "TarefaHistorico",
                column: "TarefaHistoricoId");

            migrationBuilder.AddForeignKey(
                name: "FK_TarefaHistorico_tarefas_TarefaId",
                table: "TarefaHistorico",
                column: "TarefaId",
                principalSchema: "app",
                principalTable: "tarefas",
                principalColumn: "tarefa_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
