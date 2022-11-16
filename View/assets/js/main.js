var api = "http://localhost:5220";

// inicializa todos os componentes do Materialize de uma única vez.
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
    duration: 300,
    swipeable: false
  });
  
});
