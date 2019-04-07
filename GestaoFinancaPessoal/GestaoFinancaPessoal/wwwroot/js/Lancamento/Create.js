function LancamentoChange(obj) {

    if ($("#Tipo option:selected").val() == "TRANSFERENCIA") {
        $("[Tipo]").removeClass("col-md-3", "slow", "swing");
        $("[Tipo]").addClass("col-md-4", "slow", "swing");

        $("[Conta]").removeClass("col-md-3", "slow", "swing");
        $("[Conta]").addClass("col-md-6", "slow", "swing");

        $("[ContaDestino]").show("slow", "swing");

    } else {
        $("[Tipo]").removeClass("col-md-4", 1000);
        $("[Tipo]").addClass("col-md-3", 1000);

        $("[Conta]").removeClass("col-md-6", 1000);
        $("[Conta]").addClass("col-md-3", 1000);

        $("[ContaDestino]").hide("slow", "swing");

    }
}

function CadastrarConta() {
    destino = "/Conta/ListConta";
    $("#iFrameModel").attr("src", "../Conta/Create")
    $('#Model').modal('show')
}

function CadastrarCategoria() {
    destino = "/Categoria/ListCategoria";
    $("#iFrameModel").attr("src", "../Categoria/Create")
    $('#Model').modal('show')
}

function Response(response) {
    var elements = "";
    $.each(response, function (index, value) {
        elements = elements + '<option value="' + value.id + '">' + value.nome + '</option>'
    })

    if (destino == "/Conta/ListConta") {
        $('#Conta_Id').html(elements);
        $('#ContaDestion_Id').html(elements);

    } else {
        $('#Categoria_Id').html(elements);
    }
}