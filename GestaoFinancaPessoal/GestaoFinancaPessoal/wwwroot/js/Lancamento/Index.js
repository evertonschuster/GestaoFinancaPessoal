function ExibirOcultarAtalhos() {
    var btn = $("[Lancamento]");

    if ($(btn).is(":visible")) {
        $("[Lancamento]").hide("slow", "swing");
        $("[IconeBtn]").text("add");

    } else {
        $("[Lancamento]").show("slow", "swing");
        $("[IconeBtn]").text("remove");

    }
}

function MoverBotao() {
    if ($('[Lancamento]').hasClass('float-right')) {
        $("[Lancamento]").removeClass("float-right");
        $("[LancamentoDIV]").removeClass("float-right");

        $("[Lancamento]").hide();
    } else {
        
        $("[Lancamento]").addClass("float-right");
        $("[LancamentoDIV]").addClass("float-right");
        $("[Lancamento]").hide();

    }
    

}
