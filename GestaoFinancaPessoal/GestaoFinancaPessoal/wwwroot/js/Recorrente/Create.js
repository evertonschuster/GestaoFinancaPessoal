
function AtualizaDataInicial() {
    var DataPagamento = $("#DataPagamento").val();
    var txtDataInicial = $("#DataInicial");

    txtDataInicial.val(DataPagamento);
}

function AtualizaDataFinal() {
    var Periodicidade = $("#Periodicidade").val();
    var Repetir = $("#Repetir").val();

    var Quantidade = $("#Quantidade").val();
    var ParcelaInicial = $("#ParcelaInicial").val();

    var DataInicial = $("#DataInicial").val();

    var tempoDias = (Periodicidade * Repetir) * (Quantidade - (ParcelaInicial == 0 ? ParcelaInicial : ParcelaInicial - 1));

    var dateParts = DataInicial.split("-");
    var time = new Date(dateParts[0] + "/" + dateParts[1] + "/" + dateParts[2]);

    var outraData = new Date();
    outraData.setDate(time.getDate() + tempoDias); // Adiciona 3 dias

    dateParts = outraData.toLocaleDateString().split("/");

    var dataFinall = dateParts[2] + "-" + dateParts[1] + "-" + dateParts[0];
    console.log(dataFinall);
    $("#DataFinal").val(dataFinall);

}

function CalculaValorTotal() {
    var Valor = $("#Valor").val().replace(".","").replace(",", ".");
    var txtValorTotal = $("#ValorTotal");

    var Quantidade = $("#Quantidade").val();
    var ParcelaInicial = $("#ParcelaInicial").val();

    txtValorTotal.val((Quantidade - (ParcelaInicial == 0 ? ParcelaInicial : ParcelaInicial - 1)) * Valor);

}


function CalcularPeriodo() {
    try {
        AtualizaDataInicial();
    } finally { }

}

function CalcularPeriodoFinal() {
    try {
        AtualizaDataFinal();
    } finally { }
    try {
        CalculaValorTotal();
    }
    finally { }
}

$(function () {
    $("#IsMensal").change(function (chk) {
        $("#DivPeriodicidade").hide("slow");
        $("#DivData").hide("slow");

        $("#IsMensal").val("True");
        $("#IsAvancado").val("False");

    })

    $("#IsAvancado").change(function (chk) {
        $("#DivPeriodicidade").show("slow");
        $("#DivData").show("slow");

        $("#IsMensal").val("False");
        $("#IsAvancado").val("True");
    })

})