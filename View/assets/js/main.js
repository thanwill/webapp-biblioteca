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
