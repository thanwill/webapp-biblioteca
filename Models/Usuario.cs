using Newtonsoft.Json;
namespace Biblioteca;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string CPF { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Nascimento { get; set; }

    public string Listar(BibliotecaContext banco)
    {
        return JsonConvert.SerializeObject(
            banco.Usuarios.ToList(), Formatting.Indented
        );
    }

    public string Buscar(BibliotecaContext banco, int id)
    {
        return JsonConvert.SerializeObject(banco.Usuarios.Find(id));
    }

    public int Cadastrar(BibliotecaContext banco, Usuario usuario)
    {
        bool cadExistente = Validar(banco, usuario);
        if (cadExistente == false)
        {
            banco.Usuarios.Add(usuario);
        }
        else
        {
            throw new Exception("Este usuário já foi cadastrado anteriormente");
        }
        return banco.SaveChanges();
    }

    public void Atualizar(BibliotecaContext banco, UsuarioAtualizar atualizado, int id)
    {        
        var usuario = banco.Usuarios.Find(id);
        if(usuario == null)
        {
            Console.WriteLine(Results.NotFound());            
        }
        if(null != atualizado.Nome)         usuario.Nome        = atualizado.Nome;
        if(null != atualizado.Sobrenome)    usuario.Sobrenome   = atualizado.Sobrenome;
        if(null != atualizado.CPF)          usuario.CPF         = atualizado.CPF;
        if(null != atualizado.Email)        usuario.Email       = atualizado.Email;
        if(null != atualizado.Telefone)     usuario.Telefone    = atualizado.Telefone;
        if(null != atualizado.Nascimento)   usuario.Nascimento  = atualizado.Nascimento;

        banco.SaveChanges();
        Console.WriteLine(Results.Ok());   
        
    }
    public int Deletar(BibliotecaContext banco, int id)
    {
        var usuario = banco.Usuarios.Find(id);
        if (usuario != null)
        {
            banco.Remove(usuario);
            return banco.SaveChanges();
        }
        else
        {
            throw new Exception("Talvez você esteja tentando deletar um usuário que não existe");
        }
    }

    public bool Validar(BibliotecaContext banco, Usuario usuario)
    {
        bool cadExistente = false;
        foreach (var busca in banco.Usuarios)
        {
            if (busca.CPF == usuario.CPF || busca.Email == usuario.Email)
            {
                cadExistente = true;
                break;
            }
        }

        return cadExistente;
    }

    public bool ValidarAtt(BibliotecaContext banco, Usuario usuario, int id)
    {
        var user = banco.Usuarios.Find(id);
        string mail = user.Email;
        user.Email = "";
        bool cadExistente = false;

        foreach (var busca in banco.Usuarios)
        {
            if (busca.Email == usuario.Email)
            {
                user.Email = mail;
                cadExistente = true;
                break;
            }
        }

        return cadExistente;
    }
}

public class ListarId
{
    public int Id { get; set; }
}
public class UsuarioAtualizar
{
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string CPF { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Nascimento { get; set; }
}
