using Newtonsoft.Json;
namespace Biblioteca;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string CPF { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }

    public string Listar(BibliotecaContext banco)
    {
        return JsonConvert.SerializeObject(banco.Usuarios.ToList(), Formatting.Indented);
    }

    public string Buscar(BibliotecaContext banco, int id)
    {
        return JsonConvert.SerializeObject(banco.Usuarios.Find(id));
    }

    public int Cadastrar(BibliotecaContext banco, Usuario usuario)
    {
        int cont = 0;

        bool cadExistente = Validar(banco, usuario);

        if (cadExistente == false)
        {
            usuario.Id = cont++;
            banco.Usuarios.Add(usuario);
            banco.SaveChanges();
            return "Usuário cadastrado com sucesso";
        }
        else
        {
            return "Este usuário já foi cadastrado anteriormente";
        }
    }

    public string Atualizar(BibliotecaContext banco, Usuario atualizado, int id)
    {
        bool mailExistente = ValidarAtt(banco, atualizado, id);

        if (mailExistente == false)
        {
            var usuario = banco.Usuarios.Find(id);
            usuario.Nome = atualizado.Nome;
            usuario.Email = atualizado.Email;
            usuario.Telefone = atualizado.Telefone;

            banco.SaveChanges();
            return "Usuário atualizado com sucesso";
        }
        else
        {
            throw new Exception("As alterações não foram concluidas pois já existe um email com estas informações");
        }
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
        user.Email = "@gmail.com";
        banco.SaveChanges();
        bool cadExistente = false;

        foreach (var busca in banco.Usuarios)
        {
            if (busca.Email == usuario.Email)
            {
                user.Email = mail;
                banco.SaveChanges();
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
