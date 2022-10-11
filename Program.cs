/*criar projeto:
//	dotnet new webabi -minimal -o NomeDoProjeto
//entrar na pasta:
//	cd NomeDoProjeto
//adicionar entity framework no console:
//	dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 6.0
//	dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 6.0
//	dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0
//incluir namespace do entity framework:
//	using Microsoft.EntityFrameworkCore;

    //antes de rodar o dotnet run pela primeira vez, rodar os seguintes comandos para iniciar a base de dados:

	dotnet ef migrations add InitialCreate
	dotnet ef database update
*/
using Microsoft.EntityFrameworkCore;

namespace Biblioteca;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("biblioteca") ?? "Data Source=biblioteca.db";
        builder.Services.AddSqlite<BibliotecaContext>(connectionString);
        var app = builder.Build();


        // LIVRO
        Livro livro = new Livro();
        app.MapGet("/livro", livro.Listar);
        app.MapGet("/livro/{id}", livro.ListarId);
        app.MapPut("/livro", livro.Cadastrar);
        app.MapDelete("/livro", livro.Deletar);
        app.MapPut("/atualizar/book/{id}", livro.Atualizar);

        // Usuário
        Usuario usuario = new Usuario();
        app.MapGet("/usuario", usuario.Listar);
        app.MapGet("/usuario/{id}", usuario.Buscar);
        app.MapPost("/usuario", usuario.Cadastrar);
        app.MapPut("/usuario", usuario.Atualizar);
        app.MapDelete("/usuario", usuario.Deletar);

        // Empréstimo
        Emprestimo emprestimo = new Emprestimo();
        app.MapGet("/emprestimo", emprestimo.Listar);
        app.MapGet("/emprestimo/{id}", emprestimo.Buscar);
        app.MapPost("/emprestimo", emprestimo.Cadastrar);
        app.MapPut("/emprestimo", emprestimo.Atualizar);

        app.Run();
    }
}