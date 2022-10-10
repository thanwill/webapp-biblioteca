<<<<<<< Updated upstream
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;


namespace Biblioteca;

public class Editora
{
    public int id { get; set; }
    public string Titulo { get; set; }
    
    // passa como parâmetro o banco, e realiza a listagem da tabela livros com o Json formatado usando Newtonsoft.Json.
    public string Listar(BibliotecaContext banco)
    {
        return JsonConvert.SerializeObject(banco.Editoras.ToList(), Formatting.Indented);
    }

    public string ListarId(BibliotecaContext banco, int id)
    {
        var livro = banco.Livros.Find(id);

        if (livro == null)
        {
            return "Não encontrado.";
        }
        else
        {
            return JsonConvert.SerializeObject(livro, Formatting.Indented);

        }
    }

    /*
        A função Cadastrar gera um id randomico,
        a Validar verifica se esse id já foi cadastrado antes e entrega outro em caso posítivo,
        ou permanece o primeiro gerado em caso negativo.
     */


    public string Cadastrar(BibliotecaContext banco, Livro novo)
    {
        Random rand = new Random();
        novo.id = rand.Next(100);

        var livro = banco.Livros.Find(novo.id);

        if(!(livro == null))
        {
            while(novo.id == livro.id)
            {
                novo.id = rand.Next(100);
                // realiza alteração do ID até achar algum disponível.                
            }
            banco.Livros.Add(novo);
            banco.SaveChanges();
        }
        else
        {
            banco.Livros.Add(novo);
            banco.SaveChanges();
        }
        return "O livro: " + novo.Titulo + ", foi cadastrado com sucesso!";
        // Atenção: ao cadastrar livro, não use id ou estoque no Body. Eles são implementados automaticamente.
        // Precisa fazer a validação de IDs iguais.

    }

    public string Deletar(BibliotecaContext banco, int id)
    {

        var livro = banco.Livros.Find(id);
        if(livro == null)
        {
            return "Não encontrado.";
        }
        else
        {
            banco.Livros.Remove(livro);
            banco.SaveChanges();
            return "Excluido com sucesso!";
        }
    }    

    public string Atualizar(BibliotecaContext banco, Livro atualizado, int id)
    {

        var livro = banco.Livros.Find(id);

        if(livro == null)
        {
            return "Não encontrado.";
        }
        else
        {
            livro.Titulo = atualizado.Titulo;
            livro.Autor = atualizado.Autor;
            livro.Lancamento = atualizado.Lancamento;

            banco.SaveChanges();
            return "Usuário atualizado com sucesso";
            return "Livro não encontrado!";
        }        
    }
       
}
=======
﻿using System;
using Newtonsoft.Json;

namespace Biblioteca;

public class Editora
{
    
    public int id { get; set; }
    public string nome { get; set; }
    public float receita { get; set; }
    public int fundacao { get; set; }
    public string CEO { get; set; }
    public string sede { get; set; }

    // passa como parâmetro o banco, e realiza a listagem da tabela livros com o Json formatado usando Newtonsoft.Json.
    public string Listar(BibliotecaContext banco)
    {
        return JsonConvert.SerializeObject(banco.Editora.ToList(), Formatting.Indented);
    }
}

>>>>>>> Stashed changes