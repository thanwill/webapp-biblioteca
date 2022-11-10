var url = 'http://localhost:5220/';

function cadastrar_livro()
{
    let body = 
    {
        'Titulo': document.getElementById('book_title').value,
        'Autor': document.getElementById('autor_name').value,
        'Lancamento': document.getElementById('book-Lancamento').value,
        'Estoque': document.getElementById('test5').value
    };

    fetch(url + 'livros',
    {
        'method': 'POST',
        'redirect': 'follow',
        'headers':
		{
			'Content-Type': 'application/json',
			'Accept': 'application/json'
		},
		'body': JSON.stringify(body)
    }) 

    .then((response) =>
    {
        if(response.ok)
        {
            return response.text()
        }
        else 
        {
            response.text().then((text) =>
            {
                throw new Error(text)
            })  
        }
    })       

    .then((output) =>
    {
        console.log(output);
        alert('Cadastro efetuado')
    })
    .catch((error) =>
    {
        console.log(error)
        alert('Não foi possível efetuar o cadastro')
    })
}

function listar_livros()
{
    
}