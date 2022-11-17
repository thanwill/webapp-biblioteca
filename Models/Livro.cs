using Newtonsoft.Json;

namespace Biblioteca;

public class Livro
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string Lancamento { get; set; }
    public int Estoque { get; set; } = 10;

    // passa como parâmetro o banco, e realiza a listagem da tabela livros com o Json formatado usando Newtonsoft.Json.
    public string Listar(BibliotecaContext banco)
    {
        return JsonConvert.SerializeObject(banco.Livros.ToList(), Formatting.Indented);
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
        Versão antiga, desconsiderar mas não apagar.
        public int Cadastrar(BibliotecaContext banco, Livro novo) { banco.Livros.Add(novo); return banco.SaveChanges(); }
    */
    
    
    public int Cadastrar(BibliotecaContext banco, Livro livro)
    {
        banco.Livros.Add(livro);
        return banco.SaveChanges();                
    }

    public int Deletar(BibliotecaContext banco, int id)
    {
        banco.Livros.Remove(banco.Livros.Find(id));
        return banco.SaveChanges();
    }

    public void Atualizar(BibliotecaContext banco, LivroAtualizar atualizado, int id)
    {
        var livro = banco.Livros.Find(id);

        if(livro == null)
        {
            Console.WriteLine(Results.NotFound());            
        }
        if(null != atualizado.Titulo)       livro.Titulo        = atualizado.Titulo;
        if(null != atualizado.Autor)        livro.Autor         = atualizado.Autor;
        if(null != atualizado.Lancamento)   livro.Lancamento    = atualizado.Lancamento;
        if(null != atualizado.Estoque)      livro.Estoque       = atualizado.Estoque;

        banco.SaveChanges();
        Console.WriteLine(Results.Ok()); 
    }
}

public class LivroAtualizar
{
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string Lancamento { get; set; }
    public int Estoque { get; set; }
}
