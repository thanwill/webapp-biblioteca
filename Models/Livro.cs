using Newtonsoft.Json;

namespace Biblioteca;

public class Livro
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public int Lancamento { get; set; }
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

    public int Cadastrar(BibliotecaContext banco, Livro novo)
    {
        banco.Livros.Add(novo);
        return banco.SaveChanges();
    }

    public int Deletar(BibliotecaContext banco, int id)
    {
        banco.Livros.Remove(banco.Livros.Find(id));
        return banco.SaveChanges();
    }

    public int Atualizar(BibliotecaContext banco, LivroAtualizar atualizar)
    {
        var livro = banco.Livros.Find(atualizar.Id);

        if (livro == null)
            throw new Exception("Não encontrado.");

        livro.Titulo = atualizar.Livro.Titulo;
        livro.Autor = atualizar.Livro.Autor;
        livro.Lancamento = atualizar.Livro.Lancamento;

        return banco.SaveChanges();
    }
}

public class LivroAtualizar
{
    public int Id { get; set; }
    public Livro Livro { get; set; }
}
