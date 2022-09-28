using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Biblioteca;

public class Usuario
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? CPF { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }

    public List<Usuario> listarUsuarios(BibliotecaContext banco)
    {
        return banco.usuarios.ToList();
    }

    public Usuario listarUsuario(BibliotecaContext banco, int id)
    {
        return banco.usuarios.Find(id);
    }

    public string cadastrarUsuario(BibliotecaContext banco, Usuario usuario)
    {
        int cont = 0;

        usuario.Id = cont++;
        banco.usuarios.Add(usuario);
        banco.SaveChanges();
        return "Usuário cadastrado com sucesso";
    }

    public string atualizarUsuario(BibliotecaContext banco, Usuario atualizado, int id)
    {
        var usuario = banco.usuarios.Find(id);
        usuario.Nome = atualizado.Nome;
        usuario.Email = atualizado.Email;
        usuario.Telefone = atualizado.Telefone;

        banco.SaveChanges();
        return "Usuário atualizado com sucesso";
    }

    public string deletarUsuario(BibliotecaContext banco, int id)
    {
        var usuario = banco.usuarios.Find(id);
        banco.Remove(usuario);
        banco.SaveChanges();
        return "Usuario deletado";
    }
}