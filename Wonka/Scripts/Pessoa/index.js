contador = 1;

function novaLinha(idPessoa, nome, sobrenome) {
    var id = "Pessoa" + contador;
    var linha = $('<tr id=' + id + '>');
    var colunaNome = $('<td id=nome>').text(nome);
    var colunaSobrenome = $('<td id=sobrenome>').text(sobrenome);
    var colunaRemover = $("<td>");

    var linkEditar = $("<a>").attr("href", "/Pessoa/Alterar/" + idPessoa).attr("class", "btn btn-sm btn-outline-success mr-2").text('Editar ');
    var iconeEditar = $("<i>").addClass("fas fa-user-edit");   

    linkEditar.append(iconeEditar);    

    var linkExcluir = $("<a>").attr("href", "/Delete/" + idPessoa).attr("class", "btn btn-sm btn-outline-danger").text('Excluir ');
    var iconeExcluir = $("<i>").addClass("fas fa-dumpster");

    linkExcluir.append(iconeExcluir);
    colunaRemover.append(colunaRemover);    

    linha.append(colunaNome);
    linha.append(colunaSobrenome);
    linha.append(linkEditar);
    linha.append(linkExcluir);
    linha.append(colunaRemover);  

    return linha;
}

function getJson() {
    var actionUrl = '/Pessoa/FindAll';

    $.getJSON(actionUrl, function (response) {

        if (response != null) { 
            
            for (var i = 0; i < response.ListaPessoas.length; i++) {
                
                var linha = novaLinha(response.ListaPessoas[i].Id, response.ListaPessoas[i].Nome, response.ListaPessoas[i].Sobrenome);                
                $('#tabelaPessoa tbody').append(linha);

            }            
        }
    });    
}

$(document).ready(function () {    
    getJson();
});












