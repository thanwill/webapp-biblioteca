function cadastrar_livro() {
    
    const body = {
        'Titulo': document.getElementById('titulo').value,
        'Autor': document.getElementById('autor').value,
        'Lancamento': document.getElementById('data-lancamento').value,
        'Estoque': document.getElementById('estoque').value
    };
    console.log(body)
    fetch(api + '/livros', {
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
                html: `${body.Titulo} cadastrado(a) com sucesso!`,
                inDuration: 300,
                classes:'green accent-2'
            });
        })
        //trata erro
        .catch((error) => {
            console.log(error);
            M.toast({
                html: `Não foi possível efetuar o cadastro! :(`,
                inDuration: 300,
                classes: 'red lighten-3'
            });
        });
}
function listar_livros() {

    fetch(api + '/livros')
        .then(response => response.json())
        .then((livros) => {
            let listaLivros = document.getElementById('lista-livros');
            for (const {
                Id,
                Titulo
            } of livros) {
            const itemLista = document.createElement("li");
            itemLista.setAttribute("id", `${Id}`);
            itemLista.setAttribute("class", "collection-item grey-text");
            itemLista.innerHTML = `
        <div style="text-align:left;" . id="${Id}">
          ${Titulo}
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
      listaLivros.appendChild(itemLista);
        }
        });
}

function listar_livro_especifico() {

    let titulo = document.getElementById('search').value
    fetch(api + '/livros')
        .then(response => response.json())
        .then((livros) => {
            let listaLivros = document.getElementById('lista-livros');

            let list = '';

            livros.forEach((livro, index) => {
                if (livro.Titulo.lenght != 0) {
                    if (livro.Titulo == titulo) {
                        list +=
                            `<div class="collection-item grey-text" style="text-align:left;">
                        ${livro.Titulo}
                        <a class="secondary-content modal-trigger" href="#see-forms-book"><i
                         onclick="info_livro(${livro.Id})" class="material-icons">visibility</i></a>
                        </div>`

                    }
                } else {
                    list +=
                        `<div class="collection-item grey-text">
                        Vazia.
                        </div>`
                }
            });
            listaLivros.innerHTML = list;
        })

}



function info_livro(id) {
    fetch(api + '/livros/' + id)
        .then(response => response.json())
        .then((livros) => {
            document.getElementById('mostrar-titulo').innerHTML = livros.Titulo
            document.getElementById('mostrar-autor').innerHTML = livros.Autor
            document.getElementById('mostrar-lancamento').innerHTML = livros.Lancamento
            document.getElementById('mostrar-quantidade').innerHTML = livros.Estoque;

            idLivro = livros.Id;

        })

}

function pre_atualizacao() {
    fetch(api + '/livros/' + idLivro)
        .then(response => response.json())
        .then((livros) => {

            let titulo = livros.Titulo
            let autor = livros.Autor
            let lancamento = livros.Lancamento

            let resp = ''

            do {
                resp = window.prompt('Deseja editar o titulo do livro? [S/N]');
                if (resp == 'S') {
                    titulo = window.prompt('Informe o novo título')
                }
            } while (resp != 'S' && resp != 'N');

            do {
                resp = window.prompt('Deseja editar o nome do autor do livro? [S/N]');
                if (resp == 'S') {
                    autor = window.prompt('Informe o novo nome do autor')
                }
            } while (resp != 'S' && resp != 'N');

            do {
                resp = window.prompt('Deseja editar a data de lançamento do livro? [S/N]');
                if (resp == 'S') {
                    lancamento = window.prompt('Informe a nova data de lançamento')
                }
            } while (resp != 'S' && resp != 'N');

            atualizar_livro(idLivro, titulo, autor, lancamento)
        })

}

function atualizar_livro(id, divTitulo, divAutor, divLancamento) {

    let body = {
        'Titulo': divTitulo,
        'Autor': divAutor,
        'Lancamento': divLancamento
    }

    fetch(api + "/livros/" + id, {
            'method': 'PUT',
            'redirect': 'follow',
            'headers': {
                'Content-type': 'application/json',
                'Accept': 'application/json'
            },
            'body': JSON.stringify(body)
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
            listar_livros()
            console.log(output)
            alert('Livro atualizado')
        })
        .catch((error) => {
            console.log(error)
            alert('Não foi possível atualizar o livro')
        })
}

function deletar_livro() {

    let id = idLivro;
    fetch(api + '/livros/' + id, {
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
            listar_livros()
            console.log(output)
            alert('Livro removido')
        })
        .catch((error) => {
            console.log(error)
            alert('Não foi possível remover o livro')
        })
}