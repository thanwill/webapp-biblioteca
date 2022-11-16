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
              ${id}
              <a 
                class="secondary-content modal-trigger"
                href="#formulario-emprestimo"
                onclick="visualizarEmprestimo(${id})">
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

function cadastrarEmprestimo() {
  const form = document.querySelector('#formulario-novo-emprestimo');
  form.addEventListener('onclick', function(e){
    e.preventDefault();
  });
  const body = {
    UsuarioId: Number.parseInt(
      document.getElementById("seleciona-usuario").value
    ),
    LivroId: Number.parseInt(
      document.getElementById("seleciona-livro").value
    ),
    Periodo: Number.parseInt(
      document.getElementById("novo-emprestimo-periodo").value
    ),
  };
  console.log(body)

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
      recarregarEmprestimos();
      alert("Cadastro efetuado! :D");
    })
    //trata erro
    .catch((error) => {
      console.log(error);
      alert("Não foi possível efetuar o cadastro! :(");
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
        itemLista.innerHTML = `Valor ${Titulo}`;
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
          Nome, Sobrenome
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
listarEmprestimos()