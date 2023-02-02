using Newtonsoft.Json;
namespace Biblioteca;

public class Usuario
{
    public int UsuarioId { get; set; }
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

    public string Cadastrar(BibliotecaContext banco, Usuario usuario)
    {
        int cadExistente = Validar(banco, usuario);

        if(cadExistente == 1){
            throw new Exception("CPF: "+usuario.CPF + " já cadastrado!");
        }

        if (cadExistente == 2)
        {
            throw new Exception("E-mail: "+usuario.Email + " já cadastrado!");
            
        }
        else
        {
            banco.Usuarios.Add(usuario);
        }
        banco.SaveChanges();
        return "Cadastrado com sucesso!";
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

    public int Validar(BibliotecaContext banco, Usuario usuario)
    {
        int cadExistente = 0;
        foreach (var busca in banco.Usuarios)
        {
            if (busca.CPF == usuario.CPF)
            {                
                cadExistente = 1;
                break;
            }
            if(busca.Email == usuario.Email){
                
                cadExistente = 2;
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
