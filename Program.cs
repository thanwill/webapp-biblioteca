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


        // Livro
        Livro livro = new Livro();
        app.MapGet("/livros", livro.Listar);
        app.MapPost("/livros/cadastrar", livro.Cadastrar);
        app.MapGet("/livros/deletar/{id}", livro.Excluir);
        app.MapPost("/livros/atualizar/{id}", livro.Atualizar);

        // Empréstimo
        Emprestimo emprestimo = new Emprestimo();
        app.MapGet("/emprestimos", emprestimo.Listar);
        app.MapGet("/emprestimos/{id}", emprestimo.Buscar);
        app.MapPost("/emprestimos", emprestimo.Cadastrar);
        app.MapPut("/emprestimos", emprestimo.Atualizar);
        app.MapDelete("/emprestimos/{id}", emprestimo.Excluir);

        app.Run();
    }
}
