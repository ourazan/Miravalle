﻿function LlamadoPost(url, data) {
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


function MostrarMensaje(texto) {
    alert(texto);
}

function AbrirModal(NombreControlDiv) {
    $("#" + NombreControlDiv).modal('show');
}

function OcultarModal(NombreControlDiv) {
    $("#" + NombreControlDiv).modal('hide');
}
