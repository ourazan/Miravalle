function LimpiarCampos() {
    $("#hddID").val(0);
    $("#Descripcion").val('');
    $("#CodigoReferencia").val('');
}


function CargarFormularioEdicion(categoria) {
    LimpiarCampos();
    var arreglo = categoria.toString().split('-');
    $("#hddID").val(arreglo[0]);
    $("#CodigoReferencia").val(arreglo[1]);
    $("#Descripcion").val(arreglo[2]);
    AbrirModal("divModalTipoProducto");
}

function CargarFormularioNuevo() {
  LimpiarCampos();
  AbrirModal("divModalTipoProducto");
}

function CargarResultados(Resultado) {
    MostrarMensaje(Resultado.mensaje);
    if (Resultado.data) {
        LimpiarCampos();
        OcultarModal("divModalTipoProducto");
        window.location.href = '/TipoProducto/Index';
    }
}
function ObtenerDatos() {
    return $("#divFormulario :input").serialize() + '&hddID=' + $("#hddID").val();

}
function ValidarFormulario() {
    var Mensajes = '';
    if ($("#Descripcion").val() == '' || $("#Descripcion").val() == null) {
        Mensajes += 'Debe registrar una descripción \n'
    }
    if ($("#CodigoReferencia").val() == '' || $("#CodigoReferencia").val() == null) {
        Mensajes += 'Debe registrar un código de referencia \n'
    }
    return Mensajes;
}


function Guardar() {
    var Mensajes = ValidarFormulario();

    if (Mensajes == '') {
        LlamadoPost('/TipoProducto/GuardarRegistro', ObtenerDatos());
    }
    else { MostrarMensaje(Mensajes); }
}



$(function () {
    var Adicion = $('<a href="#" class="ace-icon fa fa-plus" title="Crear nuevo registro" onclick="CargarFormularioNuevo();"  ></a>');
    $("#grdTipo th:first-child").append(Adicion);
  }
);

