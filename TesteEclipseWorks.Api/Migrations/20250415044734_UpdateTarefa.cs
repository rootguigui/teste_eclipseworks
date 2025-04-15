using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteEclipseWorks.Api.Migrations
{
    [ExcludeFromCodeCoverage]
    /// <inheritdoc />
    public partial class UpdateTarefa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Prioridade",
                schema: "app",
                table: "tarefas",
                newName: "prioridade");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                schema: "app",
                table: "tarefas",
                newName: "usuario_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_inicio",
                schema: "app",
                table: "tarefas",
                type: "timestamp",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_fim",
                schema: "app",
                table: "tarefas",
                type: "timestamp",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "usuario_id",
                schema: "app",
                table: "tarefas",
                type: "INT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_inicio",
                schema: "app",
                table: "projetos",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_fim",
                schema: "app",
                table: "projetos",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "prioridade",
                schema: "app",
                table: "tarefas",
                newName: "Prioridade");

            migrationBuilder.RenameColumn(
                name: "usuario_id",
                schema: "app",
                table: "tarefas",
                newName: "UsuarioId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_inicio",
                schema: "app",
                table: "tarefas",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_fim",
                schema: "app",
                table: "tarefas",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                schema: "app",
                table: "tarefas",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_inicio",
                schema: "app",
                table: "projetos",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_fim",
                schema: "app",
                table: "projetos",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");
        }
    }
}
