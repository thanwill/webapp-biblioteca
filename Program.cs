/*  criar projeto:
//	dotnet new webabi -minimal -o NomeDoProjeto
//  entrar na pasta:
//	cd NomeDoProjeto
//  adicionar entity framework no console:
//	dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 6.0
//	dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 6.0
//	dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0
//  incluir namespace do entity framework:
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
        /* 
        ANTES
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("Biblioteca") ?? "Data Source=Biblioteca.db";
        builder.Services.AddSqlite<BibliotecaContext>(connectionString);
        var app = builder.Build();
        
        DEPOIS */
        //cria builder da aplicacao
        var builder = WebApplication.CreateBuilder(args);
        
        //adiciona database ao builder
        builder.Services.AddSqlite<BibliotecaContext>(builder.Configuration.GetConnectionString("Biblioteca") ?? "Data Source=Biblioteca.db");
        
        //adiciona politica permissiva de cross-origin ao builder
        builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
        
        //cria aplicacao usando o builder
        var app = builder.Build();
        
        //ativa a politica de cross-origin
        app.UseCors();

        // Livro
        Livro livro = new Livro();
        app.MapGet("/livros", livro.Listar);
        app.MapGet("/livros/{id}", livro.ListarId);
        app.MapPost("/livros", livro.Cadastrar);
        app.MapDelete("/livros/{id}", livro.Deletar);
        app.MapPut("/livros/{id}", livro.Atualizar);

        // Usuário
        Usuario usuario = new Usuario();
        app.MapGet("/usuarios/{id}", usuario.Buscar);
        app.MapGet("/usuarios", usuario.Listar);
        app.MapPost("/usuarios", usuario.Cadastrar);
        app.MapPut("/usuarios", usuario.Atualizar);
        app.MapDelete("/usuarios", usuario.Deletar);

        // Empréstimo
        Emprestimo emprestimo = new Emprestimo();
        app.MapGet("/emprestimo", emprestimo.Listar);
        app.MapGet("/emprestimo/{id}", emprestimo.Buscar);
        app.MapPost("/emprestimo", emprestimo.Cadastrar);
        app.MapPut("/emprestimo", emprestimo.Atualizar);

        //roda aplicacao na porta (arbitraria)
        app.Run();
    }
}
