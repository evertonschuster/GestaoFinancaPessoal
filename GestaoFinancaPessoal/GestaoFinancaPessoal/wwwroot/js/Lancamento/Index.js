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
        $("[botoes]").removeClass("col-12");
        $("[botoes]").addClass("col-10");
        $("[Lancamento]").hide();
    } else {
        
        $("[Lancamento]").addClass("float-right");
        $("[LancamentoDIV]").addClass("float-right");
        $("[botoes]").removeClass("col-10");
        $("[botoes]").addClass("col-12");
        $("[Lancamento]").hide();

    }
    

}
