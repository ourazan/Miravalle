
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

function CargarResultados(Resultados,url) {
    if (Resultado.data) {

        if (url == '/Sede/GuardarRegistro' && $("#hddID").val() == '0') {
            MostrarMensaje('Se ha creado la sede exitosamente');
        }
        if (url == '/Sede/GuardarRegistro' && $("#hddID").val() != '0') {
            MostrarMensaje('Se ha editado la sede exitosamente');
        }
        if (url == '/Sede/EliminarRegistro') {
            MostrarMensaje('Se ha eliminado la sede exitosamente');
        }


        LimpiarCampos();
        OcultarModal("divModalSede");
        window.location.href = '/Sede/Index';
    } else {

        if (url == '/Sede/GuardarRegistro' && $("#hddID").val() == '0') {
            MostrarMensaje('No se pudo crear la sede');
        }
        if (url == '/Sede/GuardarRegistro' && $("#hddID").val() != '0') {
            MostrarMensaje('No se pudo editar la sede');
        }
        if (url == '/Sede/EliminarRegistro') {
            MostrarMensaje('No se pudo eliminar la sede');
        }
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
    if (!ValidarAccion('/Sede/ExisteSedeXInventario', "hddID=" + Tipo)) {
        LlamadoPost('/Sede/EliminarRegistro', "hddID=" + Tipo);
    }
    else {
        MostrarMensaje('No se puede eliminar la sede porque se encuentra relacionado con el inventario');
    }
}


$(function () {
    var Adicion = $('<a href="#" class="ace-icon fa fa-plus" title="Crear nuevo registro" onclick="CargarFormularioNuevo();"  ></a>');
    $("#grdSede th:first-child").append(Adicion);
}
);


