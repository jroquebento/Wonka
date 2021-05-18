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
            //Adicionar a mensagem
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

    $('.teste').popover().click(function () {
        if ($('#txtTipoDocumento').val().length == 0 || $('#txtNumeroDocumento').val().length == 0) {
            setTimeout(function () {
                $('.teste').popover('hide');
            }, 2000);
        }
    });

    
});
