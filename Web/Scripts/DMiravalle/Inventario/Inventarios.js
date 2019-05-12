$(document).ready(function () {
    //EVENTOS CONTROLES

    $('#btnAdicionar').on('click', function () {
        $('#modalAdcUsr').modal('toggle');
    });

    $('#btnCancelar').on('click', function () {
        $('#modalAdcUsr').modal('hide');
    });

    $('#btnCrear').on('click', function () {
        crearRegistro();
    });

    //FUNCIONES

    function crearRegistro() {

        LlamadoPostXMLHttp('/Inventario/GuardarRegisro', Response_crearRegistro());

    }

    function Response_crearRegistro() {

        //if (res === true) {
        //} else {
        //}

    }

});

