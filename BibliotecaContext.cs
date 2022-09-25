using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Biblioteca;

// Uma instância DbContext representa uma sessão com o banco de dados e pode ser usada para consultar e salvar instâncias de suas entidades
// Veja os métodos: https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext?view=efcore-6.0
public class BibliotecaContext: DbContext
{

    // DbContextOptions: This is an internal API that supports the Entity Framework Core infrastructure
    public BibliotecaContext(DbContextOptions options) : base(options){}

    //Se as propriedades DbSet<TEntity> tiverem um setter público, elas serão inicializadas automaticamente quando a instância do contexto derivado for criada.

    // Cria a tabela Livros. 
    public DbSet<Livro> Livros { get; set; } = null!;

    // Cria a tabela Usuarios
    public DbSet<Usuario> Usuarios { get; set; } = null!;

    // IMPORTANTES MÉTODOS

    // Add(Object) : Adicionada objeto, de modo que serão inseridas no banco de dados quando SaveChanges() for chamado
    // AddRange(IEnumerable<Object>) : Adiciona Enumeraveis objetos.


}

