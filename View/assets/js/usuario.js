function cadastrar_usuario(e) {
    let body = {
        'Nome': document.getElementById('nome').value,
        'Sobrenome': document.getElementById('sobrenome').value,
        'CPF': document.getElementById('cpf').value,
        'Email': document.getElementById('email').value,
        'Telefone': document.getElementById('telefone').value,
        'Nascimento': document.getElementById('nascimento').value,
    };
    //envio da requisicao usando a FETCH API
    //configuracao e realizacao do POST no endpoint "usuarios"
    fetch(api + "/usuario", {
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
            html.inDuration = M.toast({
                html: `${body.Nome} cadastrado(a) com sucesso!`,
                inDuration: 600
            });
        })
        //trata erro
        .catch((error) => {
            console.log(error);
            M.toast({
                html: `Não foi possível efetuar o cadastro! :(`,
                inDuration: 300,
                classes: 'red'
            });
        });
}

function listar_usuarios() {

    // da um GET no endpoint "usuarios"
    fetch(api + '/usuarios')
        .then(response => response.json())
        .then((usuarios) => {
            //pega div que vai conter a lista de usuarios
            let listaUsuarios = document.querySelector('#lista-usuarios');

            /*limpa div
            while (listarUsuarios.firstChild) {
                listarUsuarios.removeChild(listarUsuarios.firstChild);
            }*/
            let list = '';
            usuarios.forEach((usuario) => {
                if (usuario.Nome.length != 0) {
                    list +=
                        `<div class="collection-item grey-text" style="text-align:left;">
                            ${usuario.Nome} ${usuario.Sobrenome}
                            <a class="secondary-content modal-trigger registros-usuarios" value="${usuario.Id}" href="#visualizar-usuario">
                                <i class="material-icons">visibility</i>
                            </a>
                        </div>`;
                } else {
                    list +=
                        `<div class="collection-item grey-text">
                        Vazia.
                        </div>`;
                }
            });
            //preenche div com usuarios recebidos do GET
            listaUsuarios.innerHTML = list;
        });

}

function mostrar_perfil() {
    console.log('estou aqui');
    let index = document.querySelector('.registros-usuarios').value;
    // da um GET no endpoint "usuarios"

    fetch(api + `/usuario/${index}`)
        .then(response => response.json())
        .then((usuario) => {
            //pega div que vai conter a lista de usuarios
            let mostrarPerfil = document.querySelector('#mostrar-perfil');
            /*limpa div
            while (listarUsuarios.firstChild) {
                listarUsuarios.removeChild(listarUsuarios.firstChild);
            }*/
            let list = '';
            list +=
                        `
                        <table class="grey-text striped">
                            <tbody>
                            <tr>
                                <td>Nome</td>
                                <td>${usuario.Nome}</td>
                            </tr>
                            <tr>
                                <td>Sobrenome</td>
                                <td>${usuario.Sobrenome}</td>
                            </tr>
                            <tr>
                                <td>E-mail</td>
                                <td>${usuario.Email}</td>
                            </tr>
                            <tr>
                                <td>Telefone</td>
                                <td>${usuario.Telefone}</td>
                            </tr>
                            <tr>
                                <td>Nascimento</td>
                                <td>${usuario.Nascimento}</td>
                            </tr>
                            </tbody>
                        </table>
                        `;
            //preenche div com usuarios recebidos do GET
            mostrarPerfil.innerHTML = list;
        });
}
listar_usuarios();