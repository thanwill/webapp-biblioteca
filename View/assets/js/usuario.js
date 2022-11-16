function cadastrar_usuario() {
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
    fetch(api + "/usuarios", {
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

function lista_usuarios() {
    const listaUsuario = document.getElementById("lista-usuarios");
    while (listaUsuario.firstChild) {
        listaUsuario.removeChild(listaUsuario.firstChild);
    }
    fetch(api + '/usuarios')
        .then((response) => response.json())
        .then((usuarios) => {
            for (const {
                    Id, 
                    Nome, 
                    Sobrenome
                } of usuarios) {
                const itemLista = document.createElement("li");
                itemLista.setAttribute("id", `${Id}`);
                itemLista.setAttribute("class", "collection-item grey-text");
                itemLista.innerHTML = `
            <div style="text-align:left;" . id="${Id}">
              ${Nome} ${Sobrenome}
              <a 
                class="secondary-content modal-trigger"
                href="#modal-usuario"
                onclick="visualizar_perfil(${Id})">
                  <i id="see-1" class="material-icons">
                  expand_more
                  </i>
              </a>
            </div>
          `;
                listaUsuario.appendChild(itemLista);
            }
        });
}

function visualizar_perfil(id) {

    fetch(api + `/usuarios/${id}`)
        .then((response) => response.json())
        .then((usuario) => {
            const visualizaPerfil = document.querySelector('#visualizar-perfil');
            let list = 
            `
            <tbody>
            <tr>
              <td>Nome completo</td>
              <td>${usuario.Nome} ${usuario.Sobrenome}</td>
            </tr>
            <tr>
              <td>CPF</td>
              <td>${usuario.CPF}</td>
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
            `;
            visualizaPerfil.innerHTML = list;            
        });
}