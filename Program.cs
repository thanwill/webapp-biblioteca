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
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Biblioteca;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        var connectionString = builder.Configuration.GetConnectionString("Biblioteca") ?? "Data Source=Biblioteca.db";
        builder.Services.AddSqlite<BibliotecaContext>(connectionString);
        var app = builder.Build();

        
        // LIVRO
        Livro newBook = new Livro();
        app.MapGet  ("/books", newBook.Listar);
        app.MapGet  ("/book/{id}", newBook.ListarId);
        app.MapPost ("/cadastrar/book", newBook.Cadastrar);
        app.MapGet  ("/deletar/book/{id}", newBook.Deletar);
        //app.MapPost ("/atualizar/book/{id}", newBook.Atualizar);
        app.MapPut("/atualizar/book/{id}", newBook.Atualizar);

        // USUÁRIO
        Usuario newUser = new Usuario();
        app.MapGet  ("/user", newUser.Listar);
        app.MapPost ("/cadastrar/user", newUser.Cadastrar);
        app.MapGet  ("/deletar/user/{id}", newUser.Deletar);
        app.MapPost ("/atualizar/user/{id}", newUser.Atualizar);

        app.Run();
    }
}
