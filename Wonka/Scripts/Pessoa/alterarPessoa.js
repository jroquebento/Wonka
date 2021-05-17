contadorDocumento = 1;
contadorTelefone = 1;

function carregarDocumento(tr, tipo, numero,id) {
    var linha = novaLinha(tr, tipo, null, numero, id);
    $('#tabDocumento tbody').append(linha);
    contadorDocumento++;
}

//Criar função de adicionar documento
function adicionarDocumento() {
    var tipo = $('#txtTipoDocumento').val();
    var numero = $('#txtNumeroDocumento').val();
    var linha = novaLinha("Documento", tipo, null, numero);

    if (validarDocumento()) {        
        $('#tabDocumento tbody').append(linha);
        contadorDocumento++;
        limparDadosDocumento();
    }

    var documento = {
        Documento: {
            "Id" : 0,
            "Tipo": tipo,
            "Numero": numero,           
        }
    };

    post(documento, "Documento");

}

function carregarTelefone(tr, tipo, ddd, numero, id) {
    var linha = novaLinha(tr, tipo, ddd, numero, id);
    $('#tabTelefone tbody').append(linha);
    contadorTelefone++;
}

function adicionarTelefone() {
    if (validarTelefone()) {
        var tipo = $('#txtTipoTelefone').val();
        var ddd = $('#txtDDD').val();
        var numero = $('#txtNumeroTelefone').val();
        var linha = novaLinha("Telefone", tipo, ddd, numero);
        $('#tabTelefone tbody').append(linha);
        contadorTelefone++;
        limparDadosTelefone();
    }
}

function novaLinha(tr, tipo, ddd, numero, id) {
    var contador = (tr == "Documento" ? contadorDocumento : contadorTelefone);
    var idTabela = tr + contador;
    var linha = $('<tr id=' + idTabela + '>');   
    var colunaTipo = $('<td id=tipo' + tr + '>').text(tipo);
    var colunaNumero = $('<td id=numero' + tr + '>').text(numero);
    var colunaDDD = $('<td id=DDD' + tr + '>').text(ddd);
    var inputId = $('<td id=id' + tr + '><input type=hidden value=' + id + '>');
    var colunaRemover = $("<td>");

    var link = $("<a>").attr("onclick", "deletarLinha(" + idTabela + "," + id + "," + "'" + tr + "'" + ")");
    var icone = $("<i>").addClass("fas fa-trash-alt");

    link.append(icone);
    colunaRemover.append(link);

    linha.append(colunaTipo);
    tr == "Telefone" ? linha.append(colunaDDD) : null;
    linha.append(colunaNumero);
    linha.append(inputId);
    linha.append(colunaRemover);

    return linha;
}

function deletarLinha(elemento,id,tr) {
    $(elemento).remove();  
    deletar(id,tr);
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
        alert('Favor cadastrar tipo e número!');
        return false;
    }
    return true;
}

function validarTelefone() {
    if ($('#txtTipoTelefone').val().length == 0 || $('#txtDDD').val().length == 0 || $('#txtNumeroTelefone').val().length == 0) {
        alert('Favor cadastrar tipo , ddd e número!');
        return false;
    }
    return true;
}

function validarPessoa() {
    var nome = $('#txtNome').val();
    var sobrenome = $('#txtSobrenome').val();
    var trDocumento = $("#tabelaDocumento tr");

    if (nome.length == 0 || sobrenome.length == 0) {
        alert('Favor cadastrar nome!')
        return false;
    }
    if (trDocumento.length == 0) {
        alert("Favor cadastrar pelo menos 1 documento!");
        return false;
    }

    createJSON();
}

function montaJson(jsonObjeto, tr) {
    var linhas = $("#tabela" + tr + "> tr");
    if (linhas.length > 0) {
        linhas.each(function () {
            if (tr == "Documento") {
                var tipo = $(this).find("td:nth-child(1)").text();
                var numero = $(this).find("td:nth-child(2)").text();
                var idDocumento = $(this).find("td:nth-child(3) > input").val();
                var itemDocumento = {
                    Id: idDocumento,
                    Tipo: tipo,
                    Numero: numero
                }
                jsonObjeto.Documento.push(itemDocumento);
            }
            if (tr == "Telefone") {
                var tipo = $(this).find("td:nth-child(1)").text();
                var ddd = $(this).find("td:nth-child(2)").text();
                var numero = $(this).find("td:nth-child(3)").text();
                var idTelefone = $(this).find("td:nth-child(4) > input").val();
                var itemTelefone = {
                    Id: idTelefone,
                    Tipo: tipo,
                    DDD: ddd,
                    Numero: numero
                }
                jsonObjeto.Telefone.push(itemTelefone);
            }
        });
    }
}

//Criando json
function createJSON() {

    var jsonObjeto = {
        Pessoa: {
            "Id": $("#txtIdPessoa").val(),
            "Nome": $("#txtNome").val(),
            "Sobrenome": $("#txtSobrenome").val()
        },
        Endereco: {
            "Id": $("#txtIdEndereco").val(),
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

    postJson(jsonObjeto);
}

function postJson(objeto) {   
    $.ajax({
        type: 'POST',
        url: '/Pessoa/Alterar/',
        data: JSON.stringify({ jsonPessoa: objeto }),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (msg) {
            if (msg.sucesso) {
                window.location.href = "/Pessoa/Index";
            }
        },
        error: function (msg) { }
    });
}

function post(objeto, tr) {
    var idPessoa = $('#txtIdPessoa').val();
    $.ajax({
        type: 'POST',
        url: '/Pessoa/Adicionar' + tr + '/',
        data: JSON.stringify({ documento: objeto.Documento, idPessoa: idPessoa }),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (msg) {
            if (msg.sucesso) {
                msg.id
                //window.location.href = "/Pessoa/Index";
            }
        },
        error: function (msg) { }
    });
}

function deletar(id,tr) {
    $.ajax({
        type: 'POST',
        url: '/Pessoa/Deletar'+ tr + '/',
        data: JSON.stringify({ id: id }),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (msg) {
            if (msg.sucesso) {
                //window.location.href = "/Pessoa/Alterar/1";
            }
        },
        error: function (msg) { }
    });
}

function getJson() {
    var id = $('#txtIdPessoa').val();
    var actionUrl = '/Pessoa/Editar/' + id;

    $.getJSON(actionUrl, function (response) {

        if (response != null) {
            //console.log(response);

            for (var i = 0; i < response.Pessoa.length; i++) {
                $("#txtNome").val(response.Pessoa[i].Pessoa.Nome);
                $("#txtSobrenome").val(response.Pessoa[i].Pessoa.Sobrenome);
            }

            for (var i = 0; i < response.Pessoa.length; i++) {
                $("#txtTipoEndereco").val(response.Pessoa[i].Endereco.Tipo);
                $("#txtLogradouro").val(response.Pessoa[i].Endereco.Logradouro);
                $("#txtNumeroEndereco").val(response.Pessoa[i].Endereco.Numero);
                $("#txtBairro").val(response.Pessoa[i].Endereco.Bairro);
                $("#txtBairro").val(response.Pessoa[i].Endereco.Bairro);
                $("#txtCEP").val(response.Pessoa[i].Endereco.CEP);
                $("#txtCidade").val(response.Pessoa[i].Endereco.Cidade);
                $("#txtUF").val(response.Pessoa[i].Endereco.UF);
            }

            for (var i = 0; i < response.Pessoa.length; i++) {
                for (var j = 0; j < response.Pessoa[i].Telefone.length; j++) {
                    carregarTelefone("Telefone",
                        response.Pessoa[i].Telefone[j].Tipo,
                        response.Pessoa[i].Telefone[j].DDD,
                        response.Pessoa[i].Telefone[j].Numero,
                        response.Pessoa[i].Telefone[j].Id
                    );
                }
            }

            for (var i = 0; i < response.Pessoa.length; i++) {
                for (var j = 0; j < response.Pessoa[i].Documento.length; j++) {
                    carregarDocumento("Documento",
                        response.Pessoa[i].Documento[j].Tipo,
                        response.Pessoa[i].Documento[j].Numero,
                        response.Pessoa[i].Documento[j].Id
                    );
                }
            }
        }
    });
}

$(document).ready(function () {
    getJson();
});
