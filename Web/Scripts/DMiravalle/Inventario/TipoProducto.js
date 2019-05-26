function LimpiarCampos() {
    $("#hddID").val(0);
    $("#Descripcion").val('');
    $("#CodigoReferencia").val('');
    $("#Titulo").empty();
    $("#Titulo").append('Creación tipo producto');
}


function CargarFormularioEdicion(Tipo) {
    LimpiarCampos();
    var arreglo = Tipo.toString().split('-');
    $("#hddID").val(arreglo[0]);
    $("#CodigoReferencia").val(arreglo[1]);
    $("#Descripcion").val(arreglo[2]);
    AbrirModal("divModalTipoProducto");
    $("#Titulo").empty();
    $("#Titulo").append('Edición tipo producto');
}

function CargarFormularioNuevo() {
  LimpiarCampos();
  AbrirModal("divModalTipoProducto");
}

function CargarResultados(Resultado,url) {


    if (Resultado.data) {
        if (url == '/TipoProducto/GuardarRegistro' &&  $("#hddID").val()=='0') {
            MostrarMensaje('Se creo el tipo de producto exitosamente');
        }
        if (url == '/TipoProducto/GuardarRegistro' && $("#hddID").val() != '0') {
            MostrarMensaje('Se editó el tipo de producto exitosamente');
        }
        if (url == '/TipoProducto/EliminarRegistro') {
            MostrarMensaje('Se elimino el tipo de producto exitosamente');
        }
        LimpiarCampos();
        OcultarModal("divModalTipoProducto");
        window.location.href = '/TipoProducto/Index';
    } else {
        if (url == '/TipoProducto/GuardarRegistro' && $("#hddID").val() == '0') {
            MostrarMensaje('No se creo el tipo de producto exitosamente');
        }
        if (url == '/TipoProducto/GuardarRegistro' && $("#hddID").val() != '0') {
            MostrarMensaje('No se editó el tipo de producto exitosamente');
        }
        if (url == '/TipoProducto/EliminarRegistro') {
            MostrarMensaje('No se elimino el tipo de producto exitosamente');
        }
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
    if (Mensajes == '') {
        if (ValidarAccion('/TipoProducto/ExisteCodigoReferencia', 'CodigoReferencia=' + $("#CodigoReferencia").val() + '&hddID=' + $("#hddID").val())) {
            Mensajes += 'Ya se encuentra el codigo de referencia ' + $("#CodigoReferencia").val() + ' en el sistema \n'
        }
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


function EliminarTipo(Tipo) {

    if (!ValidarAccion('/TipoProducto/ExisteProductosXTipo', "hddID=" + Tipo))
    {
        LlamadoPost('/TipoProducto/EliminarRegistro', "hddID=" + Tipo); }
    else { MostrarMensaje('No se puede eliminar el tipo producto porque ya se encuentra relacionado con algunos productos'); }
}



$(function () {
    var Adicion = $('<a href="#" class="ace-icon fa fa-plus" title="Crear nuevo registro" onclick="CargarFormularioNuevo();"  ></a>');
    $("#grdTipo th:first-child").append(Adicion);
  }
);

