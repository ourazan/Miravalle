function NuevoInventario(Lote) {


}

function NuevoInventario(Lote) {


}
function ObtenerDatosInventario() {
    return $("#divFormularioInventario :input").serialize();
}

function GuardarLote() {
    var Mensajes = ValidarFormularioInventario();
    if (Mensajes == '') {
        LlamadoPost('/Inventario/GuardarRegistro', ObtenerDatosInventario());
    }
    else { MostrarMensaje(Mensajes); }
}
