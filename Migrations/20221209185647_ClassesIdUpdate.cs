using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca.Migrations
{
    public partial class ClassesIdUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Usuarios",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Livros",
                newName: "LivroId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Emprestimos",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Usuarios",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "LivroId",
                table: "Livros",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Emprestimos",
                newName: "id");
        }
    }
}
