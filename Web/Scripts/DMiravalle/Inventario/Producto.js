function CargarFormularioNuevo() {
    LimpiarCampos();
    AbrirModal("divModalProducto");
}
function CargarFormularioEdicion(categoria) {
    LimpiarCampos();
    var arreglo = categoria.toString().split('-');
    $("#hddID").val(arreglo[0]);
    $("#NombreProducto").val(arreglo[1]);
    $("#TipoProducto").val(arreglo[2]);
    AbrirModal("divModalProducto");
}
function LimpiarCampos() {
    $("#hddID").val(0);
    $("#NombreProducto").val('');
    $("#TipoProducto").val('');
}
function CargarResultados(Resultado) {
    MostrarMensaje(Resultado.mensaje);
    if (Resultado.data) {
        LimpiarCampos();
        OcultarModal("divModalProducto");
        window.location.href = '/Productos/Index';
    }
}
function ValidarFormulario() {
    var Mensajes = '';
    if ($("#NombreProducto").val() == '' || $("#NombreProducto").val() == null) {
        Mensajes += 'Debe registrar un nombre de producto \n'
    }
    if ($("#TipoProducto").val() == '' || $("#TipoProducto").val() == null) {
        Mensajes += 'Debe seleccionar un tipo de producto \n'
    }
    return Mensajes;
}

function ObtenerDatos() {
    return $("#divFormulario :input").serialize() + '&hddID=' + $("#hddID").val();
}


function Guardar() {
    var Mensajes = ValidarFormulario();

    if (Mensajes == '') {
        LlamadoPost('/Productos/GuardarRegistro', ObtenerDatos());
    }
    else { MostrarMensaje(Mensajes); }
}

$(function () {
    var Adicion = $('<a href="#" class="ace-icon fa fa-plus" title="Crear nuevo registro" onclick="CargarFormularioNuevo();"  ></a>');
    $("#grdProducto th:first-child").append(Adicion);
}
);
