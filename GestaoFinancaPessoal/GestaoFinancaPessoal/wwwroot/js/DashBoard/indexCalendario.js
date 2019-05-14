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
            valor.innerHTML = "Valor: " + json.valorPago + "/" + json.valor;
            root.appendChild(valor);

            var conta = document.createElement("Div");
            conta.innerHTML = "Conta: " + json.dsConta;
            root.appendChild(conta);
            $(root).hide();

            event.el.children[0].appendChild(root);

            event.el.addEventListener("mouseover", mouseoverShow);

            console.log(event.el);
        }

    });

    calendar.render();

    calendar.setOption('locale', 'pt-br');
});

function mouseoverShow(event) {
    console.log(event.toElement)
}



