using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca.Migrations
{
    public partial class EmprestimoRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Livroid",
                table: "Emprestimos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Usuarioid",
                table: "Emprestimos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Emprestimos_Livroid",
                table: "Emprestimos",
                column: "Livroid");

            migrationBuilder.CreateIndex(
                name: "IX_Emprestimos_Usuarioid",
                table: "Emprestimos",
                column: "Usuarioid");

            migrationBuilder.AddForeignKey(
                name: "FK_Emprestimos_Livros_Livroid",
                table: "Emprestimos",
                column: "Livroid",
                principalTable: "Livros",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Emprestimos_Usuarios_Usuarioid",
                table: "Emprestimos",
                column: "Usuarioid",
                principalTable: "Usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emprestimos_Livros_Livroid",
                table: "Emprestimos");

            migrationBuilder.DropForeignKey(
                name: "FK_Emprestimos_Usuarios_Usuarioid",
                table: "Emprestimos");

            migrationBuilder.DropIndex(
                name: "IX_Emprestimos_Livroid",
                table: "Emprestimos");

            migrationBuilder.DropIndex(
                name: "IX_Emprestimos_Usuarioid",
                table: "Emprestimos");

            migrationBuilder.DropColumn(
                name: "Livroid",
                table: "Emprestimos");

            migrationBuilder.DropColumn(
                name: "Usuarioid",
                table: "Emprestimos");
        }
    }
}
