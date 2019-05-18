document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        plugins: ['interaction', 'dayGrid', 'timeGrid', 'list'],
        height: 'auto',
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay,listWeek'
        },
        defaultView: 'dayGridMonth',
        locale: 'pt-br',
        weekNumbers: true,
        navLinks: true, // can click day/week names to navigate views
        editable: false,
        eventLimit: false, // allow "more" link when too many events
        events: '/Dashboard/GetCalendarEvent',
        eventClick: function (info) {
            window.location.href = "/lancamento/edit/" + info.event.id;
        },
        eventRender: function (event, element, view) {

            var json = event.event._def.extendedProps;

            var root = document.createElement("Div");
            root.id = "Descricao";
            var valor = document.createElement("Div");
            valor.innerHTML = "Valor: " + json.valorPago.toFixed(2) + "/" + json.valor.toFixed(2);;
            root.appendChild(valor);

            var conta = document.createElement("Div");
            conta.innerHTML = "Conta: " + json.dsConta;
            root.appendChild(conta);
            $(root).hide();

            event.el.children[0].appendChild(root);
            event.el.id = "Pai";
            event.el.addEventListener("mouseover", mouseoverShow);
            event.el.addEventListener("mouseout", mouseoverHide);

            console.log(event.el.parentElement); 
            console.log(event.el.parentNode); 
            console.log(event.el);
            console.log(event);

        }
    });

    calendar.render();

    calendar.setOption('locale', 'pt-br');
});

function mouseoverShow(event) {

    $(event.fromElement.querySelector("#Descricao")).show();

}

function mouseoverHide(event) {

    $(event.fromElement.querySelector("#Descricao")).hide();

}



