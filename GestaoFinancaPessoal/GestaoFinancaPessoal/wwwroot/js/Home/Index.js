function Logout() {


    let token = $("[name=__RequestVerificationToken]").val();
    let headers = {};
    headers["RequestVerificationToken"] = token;

    //o AJAX precissa de algum atributos para funcionar
    //URL           : o Endereco do metodo (controller/metodo)
    //TYPE          : Tipo de requisicao HTTP
    //contentType   : formato dos dados JSON
    //DATA          : os Dados
    $.ajax({
        url: "/Account/Logout",
        type: "POST",
        contentType: "application/json",
        headers: headers,                //token de validacao para a controller
        complete: function (XMLHttpRequest, status) {
            window.location.reload();
        },
        success: function (XMLHttpRequest, status) {
            window.location.reload();
        }
    }).done(function (response) {
    }

    );
}

function EnviarConfiguracaoNotificacao() {
    var Notificacao = {
        Tempo: $("#Tempo").val(),
        Periodicidade: $("#Periodicidade").val()
    }

    if (!(!isNaN(parseFloat(Notificacao.Tempo)) && isFinite(Notificacao.Tempo))) {
        $("[data-valmsg-for='Tempo']").html("<span id='Tempo - error' class=''>Informe o Tempo.</span>");
        return;
    }
    if (Notificacao.Tempo == 0 || Notificacao.Tempo < 0) {
        $("[data-valmsg-for='Tempo']").html("<span id='Tempo - error' class=''>Informe o Tempo.</span>");
        return;

    }

    let token = $("[name=__RequestVerificationToken]").val();
    let headers = {};
    headers["RequestVerificationToken"] = token;

    //o AJAX precissa de algum atributos para funcionar
    //URL           : o Endereco do metodo (controller/metodo)
    //TYPE          : Tipo de requisicao HTTP
    //contentType   : formato dos dados JSON
    //DATA          : os Dados
    $.ajax({
        url: "/Home/NotificacaoEdit",
        type: "POST",
        contentType: "application/json",
        headers: headers,                //token de validacao para a controller
        data: JSON.stringify(Notificacao),
        complete: function (XMLHttpRequest, status) {
            swal(XMLHttpRequest.responseText, "", "success").then((value) => {
                $('#exampleModal').modal('hide')
            });
        }
    }).done(function (response) {
    }

    );
}


function ClickConfig() {
    $('#exampleModal').modal('show')

    let token = $("[name=__RequestVerificationToken]").val();
    let headers = {};
    headers["RequestVerificationToken"] = token;

    //o AJAX precissa de algum atributos para funcionar
    //URL           : o Endereco do metodo (controller/metodo)
    //TYPE          : Tipo de requisicao HTTP
    //contentType   : formato dos dados JSON
    //DATA          : os Dados
    $.ajax({
        url: "/Home/NotificacaoTempoGet",
        type: "GET",
        contentType: "application/json",
        headers: headers,                //token de validacao para a controller
        complete: function (XMLHttpRequest, status, response) {
        }
    }).done(function (response) {
        console.log(response);
        $("#Tempo").val(response.tempo);
        $("#Periodicidade").val(response.periodicidade);
    }

    );
}


function SetURLIframe(url) {
    $('#iFrameForm').attr('src', url)
}

function CarregarNotificacao() {

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
        console.log(response);
        var conteudo = "";

        for (var i = 0; i < response.length; i++) {
            var item = response[i];

            var cor = "";
            if (item.strTipoLancamento == "DESPESA") {
                cor = "text-danger";

            } else if (item.strTipoLancamento == "RECEITA") {
                cor = "text-success";

            } else {
                cor = "text-info";

            }
            conteudo +=
                "<li> " +
                "   <div class='notification-content' onclick='SetURLIframe(\"lancamento/Edit/" + item.id + "\")'> " +
                "        <small class='notification-timestamp pull-right'>" + item.dataVence.substring(8, 10) + "/" + item.dataVence.substring(5, 7) + "/" + item.dataVence.substring(0, 4) + "</small> " +
                "        <div class='notification-heading'>" + item.descricao + "</div> " +
                "        <div class='notification-text " + cor + "'>" + item.strTipoLancamento + "</div> " +
                "    </div> " +
                "</li>";
        }
        console.log(conteudo);
        $("#ulNotificacoes").html(conteudo);
    }

    );
}



document.onloadend = reportWindowSize();
window.addEventListener('resize', reportWindowSize);
function reportWindowSize() {

    var windowHeight = window.innerHeight;

    $("#iFrameForm").height(windowHeight - $(".header").height() - 4);
}
