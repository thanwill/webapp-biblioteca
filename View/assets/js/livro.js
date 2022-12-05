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
            listar_livros()
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
                html: `Não foi possível efetuar o cadastro!`,
                inDuration: 300,
                classes: 'red'
            });
        });
        listar_livros();
}

function listar_livros(){
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
                <a
                    onclick="editar_livro(${livro.Id})" 
                    class="waves-effect waves-light btn modal-trigger" 
                    href="#atualiza-livro">Editar
                </a>
                <a 
                    onclick="excluir_livro(${livro.Id})"
                    class="modal-close waves-effect waves-green btn-flat">Excluir
                </a>
            </div>
            `;
            visualizaLivro.innerHTML = list;
        });
}

function editar_livro(id) {

    fetch(api + `/livros/${id}`)
        .then((response) => response.json())
        .then((livro) => {

            console.log(livro);
            const btn = document.querySelector('#btn-atualizar-livro');
            const list = `
            <div class="modal-footer">        
            <a onclick="atualizar_livro(${livro.Id})" class="modal-close waves-effect waves-green btn-flat">Atualizar</a>
            </div>
        `;    
            document.getElementById("editar-titulo").value = livro.Titulo;
            document.getElementById("editar-autor").value = livro.Autor;
            document.getElementById("editar-data-lancamento").value = livro.Lancamento;
            document.getElementById("editar-estoque").value = livro.Estoque;                    
            btn.innerHTML = list;

        });
}

function atualizar_livro(id) {

    let body = {
        'Titulo': document.getElementById('editar-titulo').value,
        'Autor': document.getElementById('editar-autor').value,
        'Lacamento': document.getElementById('editar-data-lancamento').value,
        'Estoque': document.getElementById('editar-estoque').value
    };
    fetch(api + `/livros/${id}`, {
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
            document.querySelector('#lista-livros').reload();
            M.toast({
                html: `Livro atualizado com sucesso!`,
                inDuration: 300
            });
        })
        //trata erro
        .catch((error) => {
            console.log(error);
            M.toast({
                html: `Não foi possível atualizar o livro!`,
                inDuration: 300
            });
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
listar_livros();