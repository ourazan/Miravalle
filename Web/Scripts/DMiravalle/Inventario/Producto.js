function CargarFormularioNuevo() {
    LimpiarCampos();
    AbrirModal("divModalProducto");
    $("#TituloProducto").empty();
    $("#TituloProducto").append('Creación producto');

}
function CargarFormularioEdicion(categoria) {
    LimpiarCampos();
    var arreglo = categoria.toString().split('-');
    $("#hddID").val(arreglo[0]);
    $("#NombreProducto").val(arreglo[1]);
    $("#TipoProducto").val(arreglo[2]);
    AbrirModal("divModalProducto");
    $("#TituloProducto").empty();
    $("#TituloProducto").append('Edición producto');

}
function LimpiarCampos() {
    $("#hddID").val(0);
    $("#NombreProducto").val('');
    $("#TipoProducto").val('');
}
function CargarResultados(Resultado,url) {
    MostrarMensaje('');
    if (Resultado.data) {
        if (url == '/Productos/GuardarRegistro' && $("#hddID").val() == '0') { MostrarMensaje('Se ha creado el producto exitosamente'); }
        if (url == '/Productos/GuardarRegistro' && $("#hddID").val() != '0') { MostrarMensaje('Se ha editado el producto exitosamente'); }
        if (url == '/Productos/EliminarRegistro') { MostrarMensaje('Se ha eliminado el producto exitosamente'); }

        if (url == '/Lote/GuardarRegistro' && $("#hddIDLote").val() == '0') { MostrarMensaje('Se ha creado el lote exitosamente'); }
        if (url == '/Lote/GuardarRegistro' && $("#hddIDLote").val() != '0') { MostrarMensaje('Se ha editado el lote exitosamente'); }
        if (url == '/Lote/Eliminar') { MostrarMensaje('Se ha eliminado el lote exitosamente'); }

        if (url == '/Inventario/GuardarRegistro' && $("#hddIDInventario").val() == '0') { MostrarMensaje('Se ha creado el inventario exitosamente'); }
        if (url == '/Inventario/GuardarRegistro' && $("#hddIDInventario").val() != '0') { MostrarMensaje('Se ha editado el inventario exitosamente'); }
        if (url == '/Inventario/Eliminar') { MostrarMensaje('Se ha eliminado el inventario exitosamente'); }


        LimpiarCampos();
        OcultarModal("divModalProducto");
        window.location.href = '/Productos/Index';
    }
    else {
        if (url == '/Productos/GuardarRegistro' && $("#hddID").val()=='0') { MostrarMensaje('No se pudo crear el producto'); }
        if (url == '/Productos/GuardarRegistro' && $("#hddID").val() != '0') { MostrarMensaje('No se pudo editar el producto'); }
        if (url == '/Productos/EliminarRegistro') { MostrarMensaje('No se pudo eliminar el producto'); }

        if (url == '/Lote/GuardarRegistro' && $("#hddIDLote").val() == '0') { MostrarMensaje('No se pudo crear el Lote'); }
        if (url == '/Lote/GuardarRegistro' && $("#hddIDLote").val() != '0') { MostrarMensaje('No se pudo editar el lote'); }
        if (url == '/Lote/Eliminar') { MostrarMensaje('No se pudo eliminar el lote'); }

        if (url == '/Inventario/GuardarRegistro' && $("#hddIDInventario").val() == '0') { MostrarMensaje('No se pudo crear el inventario'); }
        if (url == '/Inventario/GuardarRegistro' && $("#hddIDInventario").val() != '0') { MostrarMensaje('No se pudo editar el inventario'); }
        if (url == '/Inventario/Eliminar') { MostrarMensaje('No se pudo eliminar el inventario'); }


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

function EliminarProducto(idProducto) {
    if (!ValidarAccion('/Productos/ExisteProductoEnInventario', "hddID=" + idProducto)) {
       LlamadoPost('/Productos/EliminarRegistro', "hddID=" + idProducto);
    } else {
        MostrarMensaje('No se puede eliminar el producto porque ya se encuentra registrado en los inventarios');
    }
}