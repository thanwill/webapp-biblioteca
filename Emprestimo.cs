using Newtonsoft.Json;

namespace Biblioteca;

public class Emprestimo
{
    public int id { get; set; }
    public DateTime Inicio { get; set; }
    public DateTime Devolucao { get; set; }
    public bool Status { get; set; }
    public int Atrasos { get; set; }
    public double Custo { get; set; }
    public Livro Livro { get; set; }
    public Usuario Usuario { get; set; }

    public string Listar(BibliotecaContext banco)
    {
        return JsonConvert.SerializeObject
            (
                banco.Livros.ToList(),
                Formatting.Indented
            );
    }

    public string Buscar(BibliotecaContext banco, Buscar buscar)
    {
        return JsonConvert.SerializeObject
        (
            banco.Emprestimos
                .Find(buscar.Id), Formatting.Indented
        );
    }

    public int Cadastrar(BibliotecaContext banco, Cadastrar cadastrar)
    {
        Emprestimo emprestimo = new Emprestimo();
        emprestimo.Inicio = DateTime.Now;
        emprestimo.Devolucao = emprestimo.Inicio
            .AddDays(
                cadastrar.Periodo
            );
        emprestimo.Usuario = banco.Usuarios
            .Find(
                cadastrar.Usuario
            );
        emprestimo.Livro = banco.Livros
            .Find(
                cadastrar.Livro
            );

        banco.Emprestimos.Add(emprestimo);
        return banco.SaveChanges();
    }

    public void Atualizar(BibliotecaContext banco, Buscar buscar)
    {
        Emprestimo emprestimo = banco.Emprestimos.Find(buscar.Id)
        if (emprestimo != null)
        {

        }
        else
        {
            throw new Exception("Emprestimo não existe.");
        }

    }

    public void Excluir() { }
}

public class Cadastrar
{
    public int Usuario { get; set; }
    public int Livro { get; set; }
    public int Periodo { get; set; }
}

public class Buscar
{
    public int Id { get; set; }
}


