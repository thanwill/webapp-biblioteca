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
      M.toast({
        html: `${body.Nome} cadastrado(a) com sucesso!`,
        inDuration: 300
      });
    })
    //trata erro
    .catch((error) => {
      console.log(error);
      M.toast({
        html: `Não foi possível efetuar o cadastro! :(`,
        inDuration: 300
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
                  visibility
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
          <div class="modal-content">
          <div class="row" style="padding-top: 1vh">
          <h4 class="grey-text left-align">Usuário</h4>
          <p class="grey-text left-align">
          Consulte as informações cadastradas do usuário selecionado.
          </p>
          </div>
          <div class="row">
          <table class="grey-text striped">
          <tbody>
          <tr>
          <td>Matrícula</td>
          <td>${usuario.Id}</td>
          </tr>
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
          </table>
          </div>
          </div>
          <div class="modal-footer">
          <a onclick="editar_usuario(${usuario.Id})" class="waves-effect waves-light btn green lighten-2 modal-trigger" href="#atualiza-usuario">Editar</a>
          <a onclick="excluir_usuario(${usuario.Id})" class="waves-effect waves-red btn-flat" >Excluir</a>
          </div>
            
            `;
      visualizaPerfil.innerHTML = list;
    });
}

function editar_usuario(id) {
  
  fetch(api + `/usuarios/${id}`)
    .then((response) => response.json())
    .then((usuario) => {

      console.log(usuario);
      const btn = document.querySelector('#btn-atualizar-usuario');
      const list   = 
      `      
        <div class="modal-footer">        
          <a onclick="atualizar_usuario(${usuario.Id})" class="modal-close waves-effect waves-green btn-flat">Atualizar</a>
        </div>
      `;
      document.getElementById("editar-nome").value = usuario.Nome;
      document.getElementById("editar-sobrenome").value = usuario.Sobrenome;
      document.getElementById("editar-cpf").value = usuario.CPF;
      document.getElementById("editar-email").value = usuario.Email;
      document.getElementById("editar-telefone").value = usuario.Telefone;      
      document.getElementById("editar-nascimento").value = usuario.Nascimento;  
      btn.innerHTML = list;
           
    });
}
function atualizar_usuario(id){

  let body = {
    'Nome': document.getElementById('editar-nome').value,
    'Sobrenome': document.getElementById('editar-sobrenome').value,
    'CPF': document.getElementById('editar-cpf').value,
    'Email': document.getElementById('editar-email').value,
    'Telefone': document.getElementById('editar-telefone').value,
    'Nascimento': document.getElementById('editar-nascimento').value
  }; 
  fetch(api + `/usuarios/${id}`, {
    method: "PUT",
    redirect: "follow",
    headers: {
      "Content-Type": "application/json",
      Accept: "application/json",
    },
    body: JSON.stringify(body),
  })
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
      html: `Usuário atualizado com sucesso!`,
      inDuration: 300
    });
  })
  //trata erro
  .catch((error) => {
    console.log(error);
    M.toast({
      html: `Não foi possível atualizar o usuário!`,
      inDuration: 300
    });
  });
}

function excluir_usuario(id) {
  fetch(api + `/usuarios/${id}`, {
      'method': 'DELETE',
      'redirect': 'follow'
    })
    .then((response) => {
      if (response.ok) {
        return response.text()
      } else {
        return response.text().then((text) => {
          throw new Error(text)
        })
      }
    })
    .then((output) => {
      lista_usuarios();
      console.log(output)
      M.toast({
        html: 'Usuário removido com sucesso!',
        inDuration: 300
      });

    })
    .catch((error) => {
      console.log(error);
      M.toast({
        html: 'Não foi possível remover o usuário',
        inDuration: 300
      });
    });
}

lista_usuarios();