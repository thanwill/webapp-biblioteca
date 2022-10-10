using Newtonsoft.Json;

namespace Biblioteca;

public class Emprestimo
{
    public int id { get; set; }
    public DateTime Inicio { get; set; }
    public DateTime Devolucao { get; set; }
    public int Status { get; set; }
    public int Atrasos { get; set; }
    public double Custo { get; set; }
    public Livro Livro { get; set; }
    public Usuario Usuario { get; set; }

    public string Listar(BibliotecaContext banco)
    {
        return JsonConvert.SerializeObject
            (
                banco.Livros
                    .ToList(),
                    Formatting.Indented
            );
    }

    public string Buscar(BibliotecaContext banco, int id)
    {
        return JsonConvert.SerializeObject
        (
            banco.Emprestimos
                .Find(id), Formatting.Indented
        );
    }

    public int Cadastrar(BibliotecaContext banco, EmprestimoCadastrar cadastrar)
    {
        Emprestimo emprestimo = new Emprestimo();
        emprestimo.Inicio = DateTime.Now;
        emprestimo.Devolucao = emprestimo.Inicio
            .AddDays(
                cadastrar.Periodo
            );
        emprestimo.Usuario = banco.Usuarios
            .Find(
                cadastrar.UsuarioId
            );
        emprestimo.Livro = banco.Livros
            .Find(
                cadastrar.LivroId
            );

        banco.Emprestimos.Add(emprestimo);
        return banco.SaveChanges();
    }

    public int Atualizar(BibliotecaContext banco, EmprestimoAtualizar atualizar)
    {
        Emprestimo emprestimo = banco.Emprestimos.Find(atualizar.EmprestimoId);
        if (emprestimo != null)
        {
            emprestimo.Status = atualizar.Emprestimo.Status;
            emprestimo.Atrasos = atualizar.Emprestimo.Atrasos;
            emprestimo.Devolucao = emprestimo.Devolucao.AddDays(atualizar.Dias);
            return banco.SaveChanges();
        }
        else
        {
            throw new Exception("Emprestimo não existe.");
        }
    }
}

public class EmprestimoCadastrar
{
    public int UsuarioId { get; set; }
    public int LivroId { get; set; }
    public int Periodo { get; set; }
}

public class EmprestimoBuscar
{
    public int Id { get; set; }
}

public class EmprestimoAtualizar
{
    public int EmprestimoId { get; set; }
    public int Dias { get; set; }
    public Emprestimo Emprestimo { get; set; }
}
