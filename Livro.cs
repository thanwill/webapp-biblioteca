using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;


namespace Biblioteca;

public class Livro
{
    public int id { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public int Lancamento { get; set; }
    public int Estoque { get; set; } = 10;

    // passa como parâmetro o banco, e realiza a listagem da tabela livros com o Json formatado usando Newtonsoft.Json.
    public string listarLivros(BibliotecaContext banco)
    {
        return JsonConvert.SerializeObject(banco.Livros.ToList(), Formatting.Indented);
    }

    public string cadastrarLivro(BibliotecaContext banco, Livro novo)
    {
        Random rand = new Random();
        novo.id = rand.Next(100);
        banco.Livros.Add(novo);
        banco.SaveChanges();
        // Atenção: ao cadastrar livro, não use id ou estoque no Body. Eles são implementados automaticamente.
        // Precisa fazer a validação de IDs iguais.
        return "O livro: " + novo.Autor + ", foi cadastrado com sucesso!";
    }     
       
}
