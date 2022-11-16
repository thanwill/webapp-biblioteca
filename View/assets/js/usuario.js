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

function lista_usuarios() {
    const listaUsuario = document.getElementById("lista-usuarios");
    while (listaUsuario.firstChild) {
        listaUsuario.removeChild(listaUsuario.firstChild);
    }
    fetch(api + '/usuarios')
        .then((response) => response.json())
        .then((usuarios) => {
            usuarios.forEach(u => {
                const itemLista = document.createElement("li");
                itemLista.setAttribute("id", `${u.id}`);
                itemLista.setAttribute("class", "collection-item grey-text");
                itemLista.innerHTML = `
            <div class="exibe-titulo" style="text-align:left;">
              ${u.Nome} ${u.Sobrenome}
              <a 
                class="secondary-content modal-trigger"
                href="#formulario-emprestimo"
                onclick="visualizarEmprestimo(${u.id})">
                  <i id="see-1" class="material-icons">
                    visibility
                  </i>
              </a>
            </div>
          `;
                listaUsuario.appendChild(itemLista);
            });
        });
}
lista_usuarios();