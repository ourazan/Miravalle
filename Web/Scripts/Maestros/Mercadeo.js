function AbrirModalNuevo(accion) {
    $("#divEdicionMercadeo").modal('show');

    if (accion == '0') {
        //Para Crear
        LLimpiarCampos();
        $("#hddID").val(accion);

    }
    return false;

}

function Guardar() {
    LlamadoPost('/Mercadeo/Guardar', ObtenerDatos());
}

function CargarResultados(Resultado) {

    MostrarMensaje(Resultado.mensaje);
    if (Resultado.data > 0) {
        Cargar();
        LLimpiarCampos();
        $("#divEdicionMercadeo").modal('hide');
        window.location.href = '/Mercadeo/Index';
    }
}


function ObtenerDatos() {
    return $("#divFormulario :input").serialize() + '&hddID=' + $("#hddID").val();

}
function CargarFormularioEdicion(categoria) {
    var arreglo = categoria.toString().split('-');
    $("#hddID").val(arreglo[0]);
    $("#txtNombre").val(arreglo[1]);
    AbrirModalNuevo(arreglo[0]);
}

function Cargar() {


}

function LLimpiarCampos() {
    $("#hddID").val(0);
    $("#txtNombre").val('');
}

function MercadeoNuevo(){
    AbrirModalNuevo('0');

}
$(function () {
    var Adicion = $('<a href="#" class="ace-icon fa fa-plus"  title="Crear nuevo registro" onclick="MercadeoNuevo();"  ></a>');
    //Append it to the First cell of Header Row.
    $("#grdMercadeo th:first-child").append(Adicion);
});