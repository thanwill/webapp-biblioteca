function listarEmprestimos() {
  const listaEmprestimos = document.getElementById("lista-emprestimos");
  while (listaEmprestimos.firstChild) {
    listaEmprestimos.removeChild(listaEmprestimos.firstChild);
  }
  fetch(api + "/emprestimos")
    .then((response) => response.json())
    .then((emprestimos) => {
      for (const {
          id
        } of emprestimos) {
        const itemLista = document.createElement("li");
        itemLista.setAttribute("id", `${id}`);
        itemLista.setAttribute("class", "collection-item grey-text");
        itemLista.innerHTML = `
            <div class="exibe-titulo">
              Registro ${id}
              <a 
                class="secondary-content modal-trigger"
                href="#modal-emprestimo"
                onclick="visualizar_emprestimo(${id})">
                  <i id="see-1" class="material-icons">
                    launch
                  </i>
              </a>
            </div>
          `;
        listaEmprestimos.appendChild(itemLista);
      }
    });
}

function visualizar_emprestimo(id) {

  fetch(api + `/emprestimos/${id}`)
    .then((response) => response.json())
    .then((emprestimo) => {
      const visualizarEmprestimo = document.querySelector('#visualizar-emprestimo');
      console.log(emprestimo);

      let list =
        `
          <div class="modal-content">
          <div class="row" style="padding-top: 1vh">
            <h4 class="grey-text left-align">Empréstimo</h4>
            <p class="grey-text left-align">
              Consulte as informações cadastradas do empréstimo selecionado.
            </p>
          </div>
          <div class="row">
            <table class="grey-text striped">
            <tbody>
            <tr>
              <td>Número de registro</td>
              <td>${emprestimo.id}</td>
            </tr>
            <tr>
              <td>Cadastrado em:</td>
              <td>${emprestimo.Inicio}</td>
            </tr>
            <tr>
              <td>Devolver em:</td>
              <td>${emprestimo.Devolucao}</td>
            </tr>
            <tr>
              <td>Status:</td>
              <td>${emprestimo.Status}</td>
            </tr>
            <tr>
              <td>Dias em atraso</td>
              <td>${emprestimo.Atrasos}</td>
            </tr>
            <tr>
              <td>Multa gerada</td>
              <td>${emprestimo.Custo}</td>
            </tr>
            <tr>
              <td>Livro:</td>
              <td>
                <a 
                  class="secondary-content modal-trigger"
                  href="#modal-livro"
                  onclick="visualizar_livro(${emprestimo.LivroId})">
                  Consultar
                    <i id="see-1" class="material-icons">
                      launch
                    </i>
                </a>
            </td>
            </tr>
            <tr>
              <td>Usuário</td>
              <td>
                  <a 
                  class="secondary-content modal-trigger"
                  href="#modal-usuario"
                  onclick="visualizar_usuario(${emprestimo.UsuarioId})">
                  Consultar
                  <i class="material-icons">
                  launch
                  </i>
                  </a>
              </td>
            </tr>
          </tbody>
            </table>
          </div>
        </div>
        <div class="modal-footer">
          <a href="#formulario-usuario" class="modal-close waves-effect waves-green btn-flat">Editar</a>
          <a class="modal-close waves-effect waves-green btn-flat" onclick="foo({})">Excluir</a>
        </div>
          
          `;
      visualizarEmprestimo.innerHTML = list;
    });
}

/*Em desuso
function visualizarEmprestimo(id) {
  fetch(api + `/emprestimos/${id}`)
    .then((response) => response.json())
    .then((emprestimo) => {
      document.getElementById("emprestimo-id").value = emprestimo.id;
      document.getElementById("emprestimo-livro").value = emprestimo.Livro;
      document.getElementById("emprestimo-usuario").value = emprestimo.Usuario;
      document.getElementById("emprestimo-inicio").value = emprestimo.Inicio;
      document.getElementById("emprestimo-devolucao").value =
        emprestimo.Devolucao;
      document.getElementById("emprestimo-status").value = emprestimo.Status;
      document.getElementById("emprestimo-atrasos").value = emprestimo.Atrasos;
      document.getElementById("emprestimo-custo").value = emprestimo.Custo;
    });
}
*/

function cadastrarEmprestimo() {
  const body = {
    UsuarioId: Number.parseInt(
      document.getElementById("seleciona-usuario").value
    ),
    LivroId: Number.parseInt(
      document.getElementById("seleciona-livro").value
    ),
    Periodo: Number.parseInt(
      document.getElementById("novo-emprestimo-periodo").value
    )
  };

  fetch(api + "/emprestimos", {
      method: "POST",
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
        html: `Cadastrado com sucesso!`,
        inDuration: 300
      });
    })
    //trata erro
    .catch((error) => {
      console.log(error);
      M.toast({
        html: `Cadastrado com sucesso!`,
        inDuration: 300
      });
    });
}

function alterarEmprestimo() {
  const body = {
    EmprestimoId: Number.parseInt(
      document.getElementById("emprestimo-id").value
    ),
    Dias: Number.parseInt(document.getElementById("emprestimo-estender").value),
  };

  fetch(api + "/emprestimos", {
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
      recarregarEmprestimos();
      alert("Alteração efetuada! :D");
    })
    //trata erro
    .catch((error) => {
      console.log(error);
      alert("Não foi possível alterar o empréstimo! :(");
    });
}

function select_livros() {
  //DA UM GET NO ENDPOINT DE LISTAR USUARIOS
  fetch(api + '/livros')
    .then(response => response.json())
    .then((livros) => {
      // Pega o select vazio
      let selectLivro = document.querySelector('#seleciona-livro');
      //PREENCHE ELA COM O NOME E O ID DOS USUARIOS
      for (const {
          Id,
          Titulo
        } of livros) {
        const itemLista = document.createElement("option");
        itemLista.setAttribute("value", `${Id}`);
        itemLista.innerHTML = `${Titulo}`;
        selectLivro.appendChild(itemLista);
      }

    });
}
select_livros();

function select_usuarios() {
  //DA UM GET NO ENDPOINT DE LISTAR USUARIOS
  fetch(api + '/usuarios')
    .then(response => response.json())
    .then((usuarios) => {
      // Pega o select vazio
      let selectUsuario = document.querySelector('#seleciona-usuario');
      //PREENCHE ELA COM O NOME E O ID DOS USUARIOS
      for (const {
          Id,
          Nome,
          Sobrenome
        } of usuarios) {
        const itemLista = document.createElement("option");
        itemLista.setAttribute("value", `${Id}`);
        itemLista.innerHTML = `${Nome} ${Sobrenome}`;
        selectUsuario.appendChild(itemLista);
      }

    });
}
select_usuarios();

function recarregarEmprestimos() {
  document.getElementById("emprestimo-id").value = "";
  document.getElementById("emprestimo-livro").value = "";
  document.getElementById("emprestimo-usuario").value = "";
  document.getElementById("emprestimo-inicio").value = "";
  document.getElementById("emprestimo-devolucao").value = "";
  document.getElementById("emprestimo-status").value = "";
  document.getElementById("emprestimo-atrasos").value = "";
  document.getElementById("emprestimo-custo").value = "";
  document.getElementById("emprestimo-estender").value = "";
  document.getElementById("novo-emprestimo-livro").value = "";
  document.getElementById("novo-emprestimo-usuario").value = "";
  document.getElementById("novo-emprestimo-periodo").value = "";
  listarEmprestimos();
}
listarEmprestimos();