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
