contadorDocumento = 1;
contadorTelefone = 1;

//Criar função de adicionar documento
function adicionarDocumento() {
    if (validarDocumento()) {
        var tipo = $('#txtTipoDocumento').val();
        var numero = $('#txtNumeroDocumento').val();
        var linha = novaLinha("Documento", tipo, null, numero);
        $('#tabDocumento tbody').append(linha);
        contadorDocumento++;
        limparDadosDocumento();
    }
}

function adicionarTelefone() {
    var tipo = $('#txtTipoTelefone').val();
    var ddd = $('#txtDDD').val();
    var numero = $('#txtNumeroTelefone').val();
    var linha = novaLinha("Telefone", tipo, ddd, numero);
    $('#tabTelefone tbody').append(linha);
    contadorTelefone++;
    limparDadosTelefone();
}

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

function deletarLinha(elemento) {
    $(elemento).remove();
}

function limparDadosDocumento() {
    $('#txtTipoDocumento').val('');
    $('#txtNumeroDocumento').val('');
}

function limparDadosTelefone() {
    $('#txtTipoTelefone').val('');
    $('#txtDDD').val('');
    $('#txtNumeroTelefone').val('');
}

function validarDocumento() {
    if ($('#txtTipoDocumento').val().length == 0 || $('#txtNumeroDocumento').val().length == 0) {
        alert('Favor cadastrar tipo e número!')
        return false;
    }
    return true;
}

//Criando json
function createJSON() {
    jsonObjeto = [];

    jsonObjeto.Pessoa = {
        "Nome": $("#txtSobrenome").val(),
        "Sobrenome": $("#txtNome").val()
    };

    jsonObjeto.Endereco = {
        "Tipo": $("#txtTipoEndereco").val(),
        "Logradouro": $("#txtLogradouro").val(),
        "Numero": $("#txtNumeroEndereco").val(),
        "Bairro": $("#txtBairro").val(),
        "CEP": $("#txtCEP").val(),
        "Cidade": $("#txtCidade").val(),
        "UF": $("#txtUF").val()
    };

    //Montar JSON Documento
    jsonObjeto.Documento = [];
    montaJson("Documento");

    // Montar JSON Telefone
    jsonObjeto.Telefone = [];
    montaJson("Telefone");

    console.log(jsonObjeto);
}

function montaJson(tr) {
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
            else {
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
