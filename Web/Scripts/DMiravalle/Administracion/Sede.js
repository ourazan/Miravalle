
function CargarFormularioNuevo() {
    LimpiarCampos();
    AbrirModal("divModalSede");
}

function LimpiarCampos() {
    $("#hddID").val(0);
    $("#NombreSede").val('');
    $("#Ciudad").val('');
    $("#Dirección").val('');
    $("#Administrador").val('');
    $("#Titulo").empty();
    $("#Titulo").append('Creación sede');

}

function EditarSede(Sede) {
    LimpiarCampos();
    Sede = Sede.toString().split('|');
    $("#hddID").val(Sede[0]);
    $("#Administrador").val(Sede[1]);
    $("#NombreSede").val(Sede[2]);
    $("#Ciudad").val(Sede[3]);
    $("#Direccion").val(Sede[4]);

    AbrirModal("divModalSede");
    $("#Titulo").empty();
    $("#Titulo").append('Edición sede');
}

function CargarResultados(Resultado) {
    MostrarMensaje(Resultado.mensaje);
    if (Resultado.data) {
        LimpiarCampos();
        OcultarModal("divModalSede");
        window.location.href = '/Sede/Index';
    }
}
function ObtenerDatos() {
    return $("#divFormulario :input").serialize() + '&hddID=' + $("#hddID").val();

}
function ValidarFormulario() {
    var Mensajes = '';
    if ($("#NombreSede").val() == '' || $("#NombreSede").val() == null) {
        Mensajes += 'Debe registrar un nombre de sede \n'
    }
    if ($("#Ciudad").val() == '' || $("#Ciudad").val() == null) {
        Mensajes += 'Debe registrar una ciudad \n'
    }
    if ($("#Direccion").val() == '' || $("#Direccion").val() == null) {
        Mensajes += 'Debe registrar una dirección \n'
    }
    return Mensajes;
}
function Guardar() {
    var Mensajes = ValidarFormulario();

    if (Mensajes == '') {
        LlamadoPost('/Sede/GuardarRegistro', ObtenerDatos());
    }
    else { MostrarMensaje(Mensajes); }
}
function EliminarSede(Tipo) {
    LlamadoPost('/Sede/EliminarRegistro', "hddID=" + Tipo);
}


$(function () {
    var Adicion = $('<a href="#" class="ace-icon fa fa-plus" title="Crear nuevo registro" onclick="CargarFormularioNuevo();"  ></a>');
    $("#grdSede th:first-child").append(Adicion);
}
);


