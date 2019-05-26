function VerInventario(IdLote, NombreProducto) {
    OcultarModal("divModalDetalleLote");
    CargarFormularioInventarioConsulta(IdLote, NombreProducto)
}


function NuevoInventario(Inventario) {
    OcultarModal("divModalDetalleLote");
    CargarFormularioInventarioNuevo(Inventario);
    $("#TituloInventario").empty();
    $("#TituloInventario").append('Creación Inventario');
}

function EditarInventario(Inventario) {
    OcultarModal("divModalDetalleInventario");
    CargarFormularioInventarioEdicion(Inventario);
    $("#TituloInventario").empty();
    $("#TituloInventario").append('Edición Inventario');
}

function CargarDetallesInventarioNuevo(IdLote) {
    LimpiarCamposInventario();
    $("#hddIDInventario").val(0);
    $("#hddIDLote").val(IdLote);
}


function CargarDetallesInventario(Detalles) {
    LimpiarCamposInventario();
    Detalles = Detalles.toString().split('-');
    $("#hddIDInventario").val(Detalles[0]);
    $("#hddIDLote").val(Detalles[1]);
    $("#SedeInventario").val(Detalles[4]);
    $("#FechaRegistroInventario").val(Detalles[5]);
    $("#Cantidad").val(Detalles[6]);
    DefinirFecha("FechaRegistroInventario");

}


function LimpiarCamposInventario() {
    $("#hddIDLote").val(0);
    $("#hddIDInventario").val(0);
    $("#Cantidad").val('');
    $("#SedeInventario").val('');
    $("#FechaRegistroInventario").val('');
    $('.datepicker').datepicker({
        language: 'es',
        format: 'dd/mm/yyyy',
    });
    PermitirNumero('Cantidad');

}


function ObtenerDatosInventario() {
    return $("#divFormularioInventario :input").serialize();
}

function ValidarFormularioInventario() {
    var Mensajes = '';
    if ($("#Cantidad").val() == '' || $("#Cantidad").val() == null) {
        Mensajes += 'Debe registrar una cantidad para el inventario \n'
    }
    if ($("#FechaRegistroInventario").val() == '' || $("#FechaRegistroInventario").val() == null) {
        Mensajes += 'Debe seleccionar una fecha de registro del inventario \n'
    }
    if ($("#SedeInventario").val() == '' || $("#SedeInventario").val() == null) {
        Mensajes += 'Debe seleccionar una sede del inventario \n'
    }
    if (Mensajes == '') {
        if (ValidarAccion('/Inventario/ExisteLoteXSede', 'hddIDLote=' + $("#hddIDLote").val() + '&SedeInventario=' + $("#SedeInventario"))) {
            Mensajes += 'El lote ya se encuentra asignado a la sede seleccionada \n'
        }

    }
    return Mensajes;
}

function GuardarInventario() {
    var Mensajes = ValidarFormularioInventario();
    if (Mensajes == '') {
        LlamadoPost('/Inventario/GuardarRegistro', ObtenerDatosInventario());
    }
    else { MostrarMensaje(Mensajes); }
}


function EliminarInventario(IdInventario) {
    LlamadoPost('/Inventario/Eliminar', "IdInventario=" + IdInventario);
}
