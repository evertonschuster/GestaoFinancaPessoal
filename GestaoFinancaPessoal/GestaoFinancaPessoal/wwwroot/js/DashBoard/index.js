
window.onload = function () {
    CarregarReceitaXLancamento();

    CarregarLancamentoPendente();

    CarregarReceitaXLancamentoLine();
};


function CarregarLancamentoPendente() {
    let token = $("[name=__RequestVerificationToken]").val();
    let headers = {};
    headers["RequestVerificationToken"] = token;

    //o AJAX precissa de algum atributos para funcionar
    //URL           : o Endereco do metodo (controller/metodo)
    //TYPE          : Tipo de requisicao HTTP
    //contentType   : formato dos dados JSON
    //DATA          : os Dados
    $.ajax({
        url: "/Home/NotificacaoGet",
        type: "GET",
        contentType: "application/json",
        headers: headers,                //token de validacao para a controller
        complete: function (XMLHttpRequest, status, response) {
        }
    }).done(function (response) {

        var conteudo = "";

        for (var i = 0; i < response.length; i++) {
            var item = response[i];

            var cor = "";
            if (item.strTipoLancamento == "DESPESA") {
                cor = "danger";

            } else if (item.strTipoLancamento == "RECEITA") {
                cor = "success";

            } else {
                cor = "info";

            }
            conteudo += "<li onclick='ClickLancamento(" + item.id + ")' class='mouseHover'> " +
                " <div class='timeline-badge " + cor + "'><i class='fa fa-smile-o'></i></div> " +
                " <div class='timeline-panel' > " +
                " <div class='timeline-heading' > " +
                " <h5 class='timeline-title'>" + item.descricao + "</h5 > " +
                " </div > " +
                " <div class='timeline-body' > " +
                " <small class='notification-timestamp pull-right'>" + item.dataVence.substring(8, 10) + "/" + item.dataVence.substring(5, 7) + "/" + item.dataVence.substring(0, 4) + "</small> " +
                " <p class='text-" + cor + "'>" + item.strTipoLancamento + "</p> " +
                " </div > " +
                " </div > " +
                " </li>"
        }
        $("#LancamentoPendente").html(conteudo);

    })
}

function ClickLancamento(id) {
    window.location.href = "/lancamento/edit/" + id;
}

function CarregarReceitaXLancamento(isMensal = true) {
    var url = "";
    if (isMensal) {
        url = "../Dashboard/GetReceitaDespesasMes";
    } else {
        url = "../Dashboard/GetReceitaDespesasDia";
    }

    $("#graph").html("");
    $("#divLoadReceitaXLancamento").show    ();

    let token = $("[name=__RequestVerificationToken]").val();
    let headers = {};
    headers["RequestVerificationToken"] = token;

    //o AJAX precissa de algum atributos para funcionar
    //URL           : o Endereco do metodo (controller/metodo)
    //TYPE          : Tipo de requisicao HTTP
    //contentType   : formato dos dados JSON
    //DATA          : os Dados
    $.ajax({
        url: url,
        type: "POST",
        contentType: "application/json",
        headers: headers,                //token de validacao para a controller
        complete: function (XMLHttpRequest, status) {
        },
    }).done(function (response) {

        Morris.Bar({
            element: 'graph',
            data: response,
            xkey: 'dataLancamento',
            ykeys: ['valorReceita', 'valorDespesa'],
            labels: ['Receita', 'Despesa'],
            xLabelAngle: 60,
            barColors: ['#28A745', '#DC3545']
        });

        $("#divLoadReceitaXLancamento").hide();
    }

    );
}

function CarregarReceitaXLancamentoLine() {

    let token = $("[name=__RequestVerificationToken]").val();
    let headers = {};
    headers["RequestVerificationToken"] = token;

    //o AJAX precissa de algum atributos para funcionar
    //URL           : o Endereco do metodo (controller/metodo)
    //TYPE          : Tipo de requisicao HTTP
    //contentType   : formato dos dados JSON
    //DATA          : os Dados
    $.ajax({
        url: "../Dashboard/GetReceitaDespesasAno",
        type: "POST",
        contentType: "application/json",
        headers: headers,                //token de validacao para a controller
        complete: function (XMLHttpRequest, status) {
        },
    }).done(function (response) {


        Morris.Area({
            element: 'graphLine',
            behaveLikeLine: true,
            lineColors: ['#28A745', '#DC3545'],
            data: response,
            xkey: 'anoMesLancamento',
            ykeys: ['valorReceita', 'valorDespesa'],
            labels: ['Receita', 'Despesa'],
            xLabels: "month"
        }).on('click', function (i, row) {
            console.log(i, row);
        });

        $("#divLoadReceitaXLancamentoGraphLine").hide(); 

        //data:
        //[{ "valorDespesa": 3000.00, "valorReceita": 0.0, "dataLancamento": "marco" },
        //{ "valorDespesa": 0.0, "valorReceita": 1000.00, "dataLancamento": "marco" },
        //{ "valorDespesa": 5.30, "valorReceita": 0.0, "dataLancamento": "abril" },
        //{ "valorDespesa": 775.50, "valorReceita": 5354.00, "dataLancamento": "abril" }],
    }

    );
}

