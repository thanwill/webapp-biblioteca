var api = "http://localhost:3000";

// inicializa todos os componentes do Materilize de uma única vez.
M.AutoInit();

$(document).ready(function () {
  $(".fixed-action-btn").floatingActionButton({
    toolbarEnabled: true
  });
  $(".datepicker").datepicker({
    format: "dd/mm/yyyy",
    setDefaultDate: true,
    minDate: new Date(1900, 01, 01),
    showClearBtn: false,
  });
  $('.tabs').tabs({
    duration: 600,
    swipeable: false
  });
});