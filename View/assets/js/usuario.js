var url = 'http://localhost:3000/';

function cadastrar_usuario() {
    let body = {
        'Nome': document.getElementById('nome').value,
        'Sobrenome': document.getElementById('sobrenome').value,
        'CPF': document.getElementById('cpf').value,
        'Email': document.getElementById('email').value,
        'Telefone': document.getElementById('telefone').value,
        'Nascimento': document.getElementById('nascimento').value,
    };
    console.log(body);
    //envio da requisicao usando a FETCH API

    //configuracao e realizacao do POST no endpoint "usuarios"
    fetch(url + "usuario", {
            'method': 'POST',
            'redirect': 'follow',
            'headers': {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            },
            'body': JSON.stringify(body)
        })
        //checa se requisicao deu certo
        .then((response) => {
            if (response.ok) {
                return response.text();
            } else {
                return response.text().then((text) => {
                    throw new Error(text);
                });
            }
        })
        //trata resposta
        .then((output) => {
            console.log(output);
            M.toast({
                html: `${body.Nome} cadastrado(a) com sucesso!`,
                inDuration: 300,
                classes: 'usuarios'
            });
        })
        //trata erro
        .catch((error) => {
            console.log(error);
            alert('Não foi possível efetuar o cadastro! :(');
        });
}