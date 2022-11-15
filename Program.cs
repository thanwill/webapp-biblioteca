/*  criar projeto:
//  dotnet new webabi -minimal -o NomeDoProjeto
//  entrar na pasta:
//  cd NomeDoProjeto
//  adicionar entity framework no console:
//  dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 6.0
//  dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 6.0
//  dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0
//  incluir namespace do entity framework:
//  using Microsoft.EntityFrameworkCore;

// antes de rodar o dotnet run pela PRIMEIRA vez
// rodar os seguintes comandos para iniciar a base de dados:
// dotnet ef migrations add InitialCreate
// obs: troque "InitialCreate" por uma descrição da alteração nas tabelas
// toda vez que alterar `DbContext` ou classe que os `DbSet`s dependem
// dotnet ef database update
// obs: rode este comando toda vez que criar uma migration
// para aplicar as alterações em banco de dados / persistência.
*/
using Microsoft.EntityFrameworkCore;

namespace Biblioteca;

class Program
{
    static void Main(string[] args)
    {
        // cria builder da aplicação
        var builder = WebApplication.CreateBuilder(args);
        
        // adiciona database ao builder
        builder.Services.AddSqlite<BibliotecaContext>(builder.Configuration.GetConnectionString("Biblioteca") ?? "Data Source=Biblioteca.db");
        
        // adiciona politica permissiva de cross-origin ao builder
        builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
        
        // cria aplicação usando o builder
        var app = builder.Build();
        
        // ativa a política de cross-origin
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
        app.MapGet("/emprestimos", emprestimo.Listar);
        app.MapGet("/emprestimos/{id}", emprestimo.Buscar);
        app.MapPost("/emprestimos", emprestimo.Cadastrar);
        app.MapPut("/emprestimos", emprestimo.Atualizar);

        // Favor não usar a porta 3000 para rodar o back-end ---
        // ela é reservada para front-ends utilizando proxy reverso (NGINX, Apache, IIS, etc).
        app.Run();
    }
}
