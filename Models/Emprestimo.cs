using Newtonsoft.Json;

namespace Biblioteca;

public class Emprestimo
{
    public int Id { get; set; }
    public DateTime Inicio { get; set; }
    public DateTime Devolucao { get; set; }
    public bool Status { get; set; } = true;
    public double Custo { get; set; } = 0;
    public int LivroId { get; set; } 
    public int UsuarioId { get; set; } 
    public Livro Livro { get; set; } 
    public Usuario Usuario { get; set; } 

    public int DiasAtrasados
    {
        get
        {
            if (Devolucao >= DateTime.Now)
            {
                return 0;
            }
            else
            {
                return (int)(DateTime.Now - Devolucao).TotalDays;
            }
        }
    }


    public int Cadastrar(BibliotecaContext banco, EmprestimoCadastrar cadastrar)
    {
        Emprestimo emprestimo = new Emprestimo();

        emprestimo.Inicio = DateTime.Now;
        emprestimo.Devolucao = emprestimo.Inicio.AddDays(DateTime.Now, cadastrar.Periodo);
        
        if(cadastrar.LivroId == null){
            throw new Exception("Livro não cadastrado.");
        }else{
            emprestimo.Livro = banco.Livros.Find(cadastrar.LivroId);
            emprestimo.LivroId = emprestimo.Livro.LivroId;
        }
        if(cadastrar.UsuarioId == null){
            throw new Exception("Usuário não cadastrado.");
        }else{
            emprestimo.Usuario = banco.Usuarios.Find(cadastrar.UsuarioId);
            emprestimo.UsuarioId = emprestimo.Usuario.UsuarioId;
        }

        if(DiasAtrasados > 0){
            emprestimo.Custo = 0.5 * DiasAtrasados;
        }

        banco.Emprestimos.Add(emprestimo);
        return banco.SaveChanges();
    }

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
    /*
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
    }*/

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
