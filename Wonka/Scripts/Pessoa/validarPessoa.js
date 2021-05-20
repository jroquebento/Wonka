$(function () {
    $("#validarFormulario").validate({
        rules: {
            nome: {
                required: true
            },
            sobrenome: {
                required: true
            },
            tipoEndereco: {
                required: true
            },
            logradouro: {
                required: true
            },
            numeroEndereco: {
                required: true,
                number: true
            },
            bairro: {
                required: true
            },
            cep: {
                required: true,
                number: true
            },
            cidade: {
                required: true
            },
            uf: {
                required: true
            },
        },
        messages: {
            nome: "O campo nome é obrigatório",
            sobrenome: "O campo sobrenome é obrigatório",
        },
        errorElement: "em",
        errorPlacement: function (error, element) {            
            error.addClass("invalid-feedback");

            if (element.prop("type") === "checkbox") {
                error.insertAfter(element.parent("label"));
            } else {
                error.insertAfter(element);
            }
        },
        highlight: function (element) {
            $(element).addClass("is-invalid").removeClass("is-valid");
        },
        unhighlight: function (element) {
            $(element).addClass("is-valid").removeClass("is-invalid");
        }
    });    
});
