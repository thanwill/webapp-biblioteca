Curitiba, 2022/2

Universidade Positivo

Análise de Sistemas/Sistemas de Informação/Engenharia de Software

Desenvolvimento de Software Visual


## Especificação do Trabalho
### Diagrama de classe

<p>Este é um modelo introdutório referencial, as funções, nomes de atributos e classes podem ter sido alteradas ou descartadas na execução final do projeto.</p>

![image](https://user-images.githubusercontent.com/62673590/191557830-d39639f0-703a-470d-9040-e94c1941b671.png)
        
### Padrão de URL
<p>Ao executar o comando <code>dotnet run</code> ou <code>dotnet watch run</code>, você será direcionado ao caminho de execução da API. Conheça abaixo nossos end-points para acessar via Postman.</p>

```c#
        // Livro
        Livro livro = new Livro();
        app.MapGet("/livros", livro.Listar);
        app.MapGet("/livros/{id}", livro.ListarId);
        app.MapPost("/livros", livro.Cadastrar);
        app.MapDelete("/livros/{id}", livro.Deletar);
        app.MapPut("/livros/{id}", livro.Atualizar);

        // Usuário
        Usuario usuario = new Usuario();
        app.MapGet("/usuarios/{id}", usuario.Buscar);
        app.MapGet("/usuarios", usuario.Listar);
        app.MapPost("/usuarios", usuario.Cadastrar);
        app.MapPut("/usuarios", usuario.Atualizar);
        app.MapDelete("/usuarios/{id}", usuario.Deletar);

        // Empréstimo
        Emprestimo emprestimo = new Emprestimo();
        app.MapGet("/emprestimos", emprestimo.Listar);
        app.MapGet("/emprestimos/{id}", emprestimo.Buscar);
        app.MapPost("/emprestimos", emprestimo.Cadastrar);
        app.MapPut("/emprestimos", emprestimo.Atualizar);
```

### Modelos para cadastro usando Json
<p>Optamos por trabalhar com JSON, um modelo de texto universalmente utilizado e com grande adesão de mercado, confira agora alguns exemplos reais de cadastro por nossos end-points.</p>

```json
"Usuario":
{
    "Nome": "Carlos",
    "Sobrenome": "Fernandes",
    "CPF": "106.747.759-45",
    "Email": "carlos@yahoo.com",
    "Telefone": "",
    "Nascimento": "14/05/1997"
}
  
"Livro":
{
    "Titulo": "Designing Web APIs: Building APIs That Developers Love",
    "Autor":"Brenda Jin",
    "Lancamento": "02/10/2018",
    "Estoque": 10
}
"Emprestimo":
{
    "UsuarioId": 2,
    "LivroId":3,
    "Estoque":10
}
```

### Modelo de cadastro pelo Postman
<img width="676" alt="image" src="https://user-images.githubusercontent.com/62673590/192662962-1643a1a7-ca86-4cc2-b784-cee10fd12392.png">
