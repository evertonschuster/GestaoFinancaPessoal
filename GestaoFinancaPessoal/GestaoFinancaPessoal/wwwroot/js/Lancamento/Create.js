function LancamentoChange(obj){

     if($( "#Tipo option:selected" ).val() == "TRANSFERENCIA"){
        $("[Tipo]").removeClass("col-md-3","slow", "swing");
        $("[Tipo]").addClass("col-md-4","slow", "swing");

        $("[Conta]").removeClass("col-md-3","slow", "swing");
        $("[Conta]").addClass("col-md-6","slow", "swing");

        $("[ContaDestino]").show("slow", "swing");

     }else{
        $("[Tipo]").removeClass("col-md-4",1000);
        $("[Tipo]").addClass("col-md-3",1000);

        $("[Conta]").removeClass("col-md-6",1000);
        $("[Conta]").addClass("col-md-3",1000);

        $("[ContaDestino]").hide("slow", "swing");

}
}