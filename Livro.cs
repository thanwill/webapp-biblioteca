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

    /*
        A função Cadastrar gera um id randomico,
        a Validar verifica se esse id já foi cadastrado antes e entrega outro em caso posítivo,
        ou permanece o primeiro gerado em caso negativo.
     */


    public int Cadastrar(BibliotecaContext banco, Livro novo)
    {
        banco.Livros.Add(novo);
        return banco.SaveChanges();
    }

    public int Deletar(BibliotecaContext banco, Deletar deletar)
    {
        banco.Livros.Remove(banco.Livros.Find(deletar.Id));
        return banco.SaveChanges();
    }

    public int Atualizar(BibliotecaContext banco, EmprestimoAtualizar atualizar)
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

public class Deletar
{
    public int Id { get; set; }
}

public class EmprestimoAtualizar
{
    public int Id { get; set; }
    public Livro Livro { get; set; }
}
