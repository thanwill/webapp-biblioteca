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
                classes: 'green accent-2'
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
    let listaLivros = document.getElementById('lista-livros');
    while (listaLivros.firstChild) {
        listaLivros.removeChild(listaLivros.firstChild);
    }
    fetch(api + '/livros')
        .then(response => response.json())
        .then((livros) => {

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
            href="#modal-livro"
            onclick="visualizar_livro(${Id})">
              <i id="see-1" class="material-icons">
              visibility
              </i>
          </a>
        </div>
      `;
                listaLivros.appendChild(itemLista);
            }
        });
}

function visualizar_livro(id) {

    fetch(api + `/livros/${id}`)
        .then((response) => response.json())
        .then((livro) => {
            const visualizaLivro = document.querySelector('#visualizar-livro');
            let list = `
            <div class="modal-content">
            <div class="row" style="padding-top: 1vh">
                <h4 class="grey-text left-align">Livros</h4>
                <p class="grey-text left-align">
                Consulte as informações cadastradas do livro selecionado.
                </p>
            </div>
            <div class="row">
                <table class="grey-text striped">
                    <tbody>
                        <tr>
                            <td>Matrícula</td>
                            <td>${livro.Id}</td>
                        </tr>
                        <tr>
                            <td>Título:</td>
                            <td>${livro.Titulo}</td>
                        </tr>
                        <tr>
                            <td>Autor:</td>
                            <td>${livro.Autor}</td>
                        </tr>
                        <tr>
                            <td>Lançamento:</td>
                            <td>${livro.Lancamento}</td>
                        </tr>
                        <tr>
                            <td>Quantidade em estoque:</td>
                            <td>${livro.Estoque}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
            </div>
            <div class="modal-footer">
                <a href="#!" class="modal-close waves-effect waves-green btn-flat">Editar</a>
                <a onclick="excluir_livro(${livro.Id})" class="modal-close waves-effect waves-green btn-flat">Excluir</a>
            </div>
            `;
            visualizaLivro.innerHTML = list;
        });
}

function excluir_livro(id) {
    fetch(api + `/livros/${id}`, {
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
            console.log(output);
            M.toast({
                html: 'Usuário removido com sucesso!',
                inDuration: 300,
                classes: 'green accent-2'
            });

        })
        .catch((error) => {
            console.log(error);
            M.toast({
                html: 'Não foi possível remover o usuário',
                inDuration: 300,
                classes: 'red lighten-3'
            });
        });
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

listar_livros();