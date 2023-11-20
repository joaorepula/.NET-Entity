using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StatusDescricao = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "Tarefas",
                columns: table => new
                {
                    TarefaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", nullable: true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CategoriaId = table.Column<int>(type: "INTEGER", nullable: false),
                    StatusId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarefas", x => x.TarefaId);
                    table.ForeignKey(
                        name: "FK_Tarefas_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tarefas_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "CategoriaId", "CriadoEm", "Nome" },
                values: new object[] { 1, new DateTime(2023, 11, 21, 19, 23, 19, 763, DateTimeKind.Local).AddTicks(5932), "Trabalho" });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "CategoriaId", "CriadoEm", "Nome" },
                values: new object[] { 2, new DateTime(2023, 11, 22, 19, 23, 19, 763, DateTimeKind.Local).AddTicks(5953), "Estudos" });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "CategoriaId", "CriadoEm", "Nome" },
                values: new object[] { 3, new DateTime(2023, 11, 23, 19, 23, 19, 763, DateTimeKind.Local).AddTicks(5955), "Lazer" });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "StatusId", "StatusDescricao" },
                values: new object[] { 1, "Não iniciada" });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "StatusId", "StatusDescricao" },
                values: new object[] { 2, "Em andamento" });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "StatusId", "StatusDescricao" },
                values: new object[] { 3, "Concluída" });

            migrationBuilder.InsertData(
                table: "Tarefas",
                columns: new[] { "TarefaId", "CategoriaId", "CriadoEm", "Descricao", "StatusId", "Titulo" },
                values: new object[] { 1, 1, new DateTime(2023, 11, 27, 19, 23, 19, 763, DateTimeKind.Local).AddTicks(6012), "Terminar relatório para reunião", 1, "Concluir relatório" });

            migrationBuilder.InsertData(
                table: "Tarefas",
                columns: new[] { "TarefaId", "CategoriaId", "CriadoEm", "Descricao", "StatusId", "Titulo" },
                values: new object[] { 2, 2, new DateTime(2023, 11, 23, 19, 23, 19, 763, DateTimeKind.Local).AddTicks(6016), "Preparar-se para a aula de Angular", 2, "Estudar Angular" });

            migrationBuilder.InsertData(
                table: "Tarefas",
                columns: new[] { "TarefaId", "CategoriaId", "CriadoEm", "Descricao", "StatusId", "Titulo" },
                values: new object[] { 3, 3, new DateTime(2023, 12, 4, 19, 23, 19, 763, DateTimeKind.Local).AddTicks(6019), "Dar um passeio relaxante no parque", 3, "Passeio no parque" });

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_CategoriaId",
                table: "Tarefas",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_StatusId",
                table: "Tarefas",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tarefas");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
