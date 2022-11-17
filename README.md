Curitiba, 2022/2

Universidade Positivo

Análise de Sistemas/Sistemas de Informação/Engenharia de Software

Desenvolvimento de Software Visual


## Fase 1: Web API Minimal
<p>Preparamos a seguir, uma breve introdução para entender o processo de desenvolvimento deste trabalho, confira! </p>

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
<p>Não sabe como consultar uma Web API? Nós também não sabiamos! Abaixo, segue um modelo para realizar uma requisição do tipo POST.</p>
<img width="461" alt="image" src="https://user-images.githubusercontent.com/62673590/202319206-b34d20c8-f42b-4b45-a63b-95c8073fd9b5.png">

## Fase 2: Front-end
<p>Agora, vamos realizar algumas dicas de como você pode trabalhar com este projeto através da branch <code>main</code>, sem ter que usar as requisições diretas da Web API criada na fase 1.</p>

### Começando
<p>Diminua a sua tela para ter acesso a um modelo responsivo usando https://materializecss.com/about.html</p>

https://user-images.githubusercontent.com/62673590/202320303-c7677eb4-e1cd-40db-affa-7dbc2f313a8c.mp4

### Conheçendo a interface
<p>Conheça algumas das funções básicas de interação com o app.</p>

https://user-images.githubusercontent.com/62673590/202321936-b360f78f-044d-41d5-a50f-299f71a331b1.mp4

### Cadastrando livro
https://user-images.githubusercontent.com/62673590/202322332-5f35ced8-2124-46ef-95f9-4eb4d86a22f9.mp4

### Cadastrando usuário


https://user-images.githubusercontent.com/62673590/202322546-ba6c9e2f-1bf2-4fe6-8a03-f2aa2855639f.mp4


### Cadastrando empréstimo

