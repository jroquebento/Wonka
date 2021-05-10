function novaLinha(tr, tipo, ddd, numero) {
    var contador = (tr == "Documento" ? contadorDocumento : contadorTelefone);
    var id = tr + contador;
    var linha = $('<tr id=' + id + '>');
    var colunaTipo = $('<td id=tipo' + tr + '>').text(tipo);
    var colunaNumero = $('<td id=numero' + tr + '>').text(numero);
    var colunaDDD = $('<td id=DDD' + tr + '>').text(ddd);
    var colunaRemover = $("<td>");

    var link = $("<a>").attr("href", "javascript:deletarLinha(" + id + ")");
    var icone = $("<i>").addClass("fas fa-trash-alt");

    link.append(icone);
    colunaRemover.append(link);

    linha.append(colunaTipo);
    tr == "Telefone" ? linha.append(colunaDDD) : null;
    linha.append(colunaNumero);
    linha.append(colunaRemover);

    return linha;
}
//Criando json
function createJSON() {

    var jsonObjeto = {
        Pessoa: {
            "Nome": $("#txtNome").val(),
            "Sobrenome": $("#txtSobrenome").val()
        },
        Endereco: {
            "Tipo": $("#txtTipoEndereco").val(),
            "CEP": $("#txtCEP").val(),
            "Logradouro": $("#txtLogradouro").val(),
            "Numero": $("#txtNumeroEndereco").val(),
            "Bairro": $("#txtBairro").val(),
            "Cidade": $("#txtCidade").val(),
            "UF": $("#txtUF").val()
        },
        Documento: [],
        Telefone: [],
    };

    montaJson(jsonObjeto, "Documento");
    montaJson(jsonObjeto, "Telefone");

    postJson(jsonObjeto, "Inserir");
}
function montaJson(jsonObjeto, tr) {
    var linhas = $("#tabela" + tr + "> tr");
    if (linhas.length > 0) {
        linhas.each(function () {
            var tipo = $(this).find("td:nth-child(1)").text();

            if (tr == "Documento") {
                var numero = $(this).find("td:nth-child(2)").text();
                var itemDocumento = {
                    Tipo: tipo,
                    Numero: numero
                }
                jsonObjeto.Documento.push(itemDocumento);
            }
            if (tr == "Telefone") {
                var ddd = $(this).find("td:nth-child(2)").text();
                var numero = $(this).find("td:nth-child(3)").text();
                var itemTelefone = {
                    Tipo: tipo,
                    DDD: ddd,
                    Numero: numero
                }
                jsonObjeto.Telefone.push(itemTelefone);
            }
        });
    }
}

function getJson() {
    $.ajax({
        type: 'GET',
        url: metodo,
        //data: JSON.stringify({ jsonPessoa: objeto }),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (msg) {
            //console.log(msg);
            if (msg.sucesso) {
                window.location.href = "Index";
            }
        },
        error: function (msg) { }
    });
}

$(document).ready(function () {
    getJson();
});



