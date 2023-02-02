using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca.Migrations
{
    public partial class DiasAtrasadosEmprestimos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Atrasos",
                table: "Emprestimos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Atrasos",
                table: "Emprestimos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
