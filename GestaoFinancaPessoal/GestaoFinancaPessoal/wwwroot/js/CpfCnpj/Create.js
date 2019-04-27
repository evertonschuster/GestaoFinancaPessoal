
//define a mascara padrao CPF
$('#CpfCnpj').mask('000.000.000-00');

function SelecionaMascara(par) {
    if (par == 0) {
        $('#CpfCnpj').mask('000.000.000-00');
        $("#RGRow").show("slow");
        $("#NomeContatoRow").hide("slow");


        $("#TipoPessoaRow").removeClass("col-md-6",  "slow");
        $("#CpfCnpjRow").removeClass("col-md-6",  "slow");

        $("#TipoPessoaRow").addClass("col-md-4",  "slow");
        $("#CpfCnpjRow").addClass("col-md-4",  "slow");
    }
    else {
        $('#CpfCnpj').mask('00.000.000/0000-00');
        $("#RGRow").hide("slow");
        $("#NomeContatoRow").show("slow");


        $("#TipoPessoaRow").removeClass("col-md-4",  "slow");
        $("#CpfCnpjRow").removeClass("col-md-4",  "slow");

        $("#TipoPessoaRow").addClass("col-md-6",  "slow");
        $("#CpfCnpjRow").addClass("col-md-6",  "slow");
    }
}

function VereficaTipoPessoa(obj) {
    if (obj.value.length > 14) {
        SelecionaMascara(1);
        $("#TipoPessoa").val("1");
    } else {
        SelecionaMascara(0);
        $("#TipoPessoa").val("0");
    }
}