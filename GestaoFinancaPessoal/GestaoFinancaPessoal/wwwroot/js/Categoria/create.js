function VereficaTipoCategoria(obj) {

    if (obj.value == 0) {
        $("#lblNome").html("Categoria");
    } else {
        $("#lblNome").html("SubCategoria");
    }

}


VereficaTipoCategoria($("#Hierarquia_Id"));