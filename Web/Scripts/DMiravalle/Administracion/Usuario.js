function CargarFormularioNuevo() {
    LimpiarCampos();
    AbrirModal("divModalUsuario");
}
function LimpiarCampos() {
    $("#hddID").val(0);
    $("#Nombre").val('');
    $("#Apellido").val('');
    $("#Correo").val('');
    $("#Sede").val('');
    $("#Usuario").val('');
    $("#Titulo").empty();
    $("#Titulo").append('Creación usuario');
    $("#divClave").attr('class', 'form-group row');
}


function EditarUsuario(Usuario) {
    LimpiarCampos();
    Usuario = Usuario.toString().split('|');
    $("#hddID").val(Usuario[0]);
    $("#Nombre").val(Usuario[1]);
    $("#Apellido").val(Usuario[2]);
    $("#Correo").val(Usuario[3]);
    $("#Sede").val(Usuario[4]);
    $("#Usuario").val(Usuario[5]);
    $("#Usuario").attr('disabled', '');
    $("#divClave").attr('class', 'hidden');

    
    AbrirModal("divModalUsuario");
    $("#Titulo").empty();
    $("#Titulo").append('Edición usuario');
}

function CargarResultados(Resultado) {
    MostrarMensaje(Resultado.mensaje);
    if (Resultado.data) {
        LimpiarCampos();
        OcultarModal("divModalUsuario");
        window.location.href = '/Usuario/Index';
    }
}

function ObtenerDatos() {
    return $("#divFormulario :input").serialize() + '&hddID=' + $("#hddID").val();
}

function ValidarFormulario(EsNuevo) {
    var Mensajes = '';
    if ($("#Nombre").val() == '' || $("#Nombre").val() == null) {
        Mensajes += 'Debe registrar un nombre para el usuario \n'
    }
    if ($("#Apellido").val() == '' || $("#Apellido").val() == null) {
        Mensajes += 'Debe registrar un apellido \n'
    }
    if ($("#Correo").val() == '' || $("#Correo").val() == null) {
        Mensajes += 'Debe registrar un correo para el usuario \n'
    }
    if ($("#Usuario").val() == '' || $("#Usuario").val() == null) {
        Mensajes += 'Debe registrar un nombre de usuario para el registro \n'
    }

    if (EsNuevo) {
        if ($("#Clave").val() == '' || $("#Clave").val() == null) {
            Mensajes += 'Debe agregar una clave para el usuario nuevo \n'
        }
    }
    return Mensajes;
}
function Guardar() {
    var Mensajes = ValidarFormulario($("#hddID").val()=='0');

    if (Mensajes == '') {
        LlamadoPost('/Usuario/GuardarRegistro', ObtenerDatos());
    }
    else { MostrarMensaje(Mensajes); }
}
function EliminarUsuario(Usuario) {
    LlamadoPost('/Usuario/Eliminar', "hddID=" + Usuario);
}

$(function () {
    var Adicion = $('<a href="#" class="ace-icon fa fa-plus" title="Crear nuevo registro" onclick="CargarFormularioNuevo();"  ></a>');
    $("#grdUsuario th:first-child").append(Adicion);
}
);


