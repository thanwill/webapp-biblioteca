using Newtonsoft.Json;

namespace Biblioteca;

public class Emprestimo
{
    public int id { get; set; }
    public DateTime Inicio { get; set; }
    public DateTime Devolucao { get; set; }
    public bool Status { get; set; } = true;
    public int Atrasos { get; set; }
    public double Custo { get; set; }
    public Livro Livro { get; set; }
    public Usuario Usuario { get; set; }

    public string Listar(BibliotecaContext banco)
    {
        return JsonConvert.SerializeObject(
            banco.Emprestimos.ToList(), Formatting.Indented
        );
    }
    public string Buscar(BibliotecaContext banco, int id)
    {
        var emprestimo = banco.Emprestimos.Find(id);
        
        if (emprestimo == null)
        {
            return "[Emprestimo:31] Não encontrado!";
        }
        else
        {
            return JsonConvert.SerializeObject(emprestimo, Formatting.Indented);
        }
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
}
