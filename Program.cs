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
        
        var connectionString = builder.Configuration.GetConnectionString("Livros") ?? "Data Source=Livros.db";
        builder.Services.AddSqlite<BibliotecaContext>(connectionString);
        var app = builder.Build();


        // LIVRO
        Livro novo = new Livro();
        app.MapGet  ("/livros", novo.Listar);
        app.MapPost ("/livros/cadastrar", novo.Cadastrar);
        app.MapGet  ("/livros/deletar/{id}", novo.Excluir);
        app.MapPost ("/livros/atualizar{id}", novo.Atualizar);

        app.Run();
    }
}
