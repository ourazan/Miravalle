function LlamadoPost(url, data) {
    $.post(url
        , data
        , function (data, status) {
            CargarResultados(data);
        });

}

function LlamadoPostXMLHttp(url, data,retornaValor) {
    var xhr = new XMLHttpRequest();
    xhr.open('POST', url);
    xhr.send(data);
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4 && xhr.status === 200) {
            if (retornaValor) {
                return JSON.parse(xhr.response);
            } else {
                CargarResultados(JSON.parse(xhr.response));
            }
        }
    }
}

function DefinirFecha(Control) {
    $('#' + Control).datepicker("destroy")
    $('#' + Control).datepicker({
        language: 'es',
        format: 'dd/mm/yyyy',
        defaultDate: $('#' + Control).val()
    });
    $('#' + Control).datepicker("setDate", $('#' + Control).val());
}

function MostrarMensaje(texto) {
    alert(texto);
}

function AbrirModal(NombreControlDiv) {
    $("#" + NombreControlDiv).modal('show');
}

function OcultarModal(NombreControlDiv) {
    $("#" + NombreControlDiv).modal('hide');
}

function FiltroTexto(Expresion, Control) {
    $("#" + Control).on("keypress keyup blur", function (event) {
        $(this).val($(this).val().replace(Expresion, ""));
        if ((event.which < 48 || event.which > 57)) {
            event.preventDefault();
        }
    });
}

function PermitirNumero(Control) {
    FiltroTexto(/[^\d].+/, Control);
}

