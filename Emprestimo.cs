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

    public Emprestimo()
    {
        Inicio = new DateTime();
    }

    public string Listar(BibliotecaContext banco)
    {
        return JsonConvert.SerializeObject(banco.Livros.ToList(), Formatting.Indented);
    }

    public string Buscar(BibliotecaContext banco, int id)
    {
        return JsonConvert.SerializeObject(banco.Emprestimos.Find(id), Formatting.Indented);
    }

    public void Cadastrar(BibliotecaContext banco, int usuarioId, int livroId, int dias)
    {
        Emprestimo emprestimo = new Emprestimo();
        emprestimo.Inicio = DateTime.Now;
        emprestimo.Devolucao = emprestimo.Inicio.AddDays(dias);
        emprestimo.Livro = banco.Livros.Find(livroId);
        emprestimo.Usuario = banco.Usuarios.Find(usuarioId);
            
        banco.Emprestimos.Add(emprestimo);
    }

    public void Atualizar(BibliotecaContext banco, int id) { }

    public void Excluir() { }
}
