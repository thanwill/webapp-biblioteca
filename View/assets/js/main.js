// inicializa todos os componentes do Materilize de uma única vez.

M.AutoInit();
$(document).ready(function () {
  $(".fixed-action-btn").floatingActionButton({
    toolbarEnabled: true,
  });
  $(".datepicker").datepicker({
    format: "dd/mm/yyyy",
    setDefaultDate: true,
    defaultDate: new Date(2022, 10, 01),
    minDate: new Date(1900, 01, 01),
    showClearBtn: false,
  });
});

let form_livro = document.querySelector("#livro-form");

let titulo = document.querySelector("#livro-titulo");
let autor = document.querySelector("#livro-autor");
let lancamento = document.querySelector("#livro-ano");
let quantidade = document.querySelector("#livro-estoque");
let btn_cadastrar = document.querySelector("#btn-cadastrar");

btn_cadastrar.addEventListener("click", function () {
  let livros = {
    titulo: titulo.value,
    autor: autor.value,
    lancamento: lancamento.value,
    quantidade: quantidade.value,
  };
  fetch("https://localhost:7185/livros", {
    method: "POST",
    body: JSON.stringify(livros),
  })
    .then((result) => {
      return result.json();
    })
    .catch((err) => {
      return console.log("erro" + err);
    });
});
