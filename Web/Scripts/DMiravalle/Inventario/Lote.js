function CargarFormularioNuevoLote(IdProducto) {
    LimpiarCamposLote();
    $("#hddIDLote").val(0);
    $("#hddIDProducto").val(IdProducto);
    $("#TituloLote").empty();
    $("#TituloLote").append('Creación Lote');

}

function EditarLote(Detalles) {
    OcultarModal("divModalDetalleLote");
     CargarFormularioEdicionLoteModificacion(Detalles);
     $("#TituloLote").empty();
     $("#TituloLote").append('Edición Lote');

}

function CargarFormularioEdicionLote(Detalles) {
    LimpiarCamposLote();
    Detalles = Detalles.toString().split('-');
    $("#hddIDLote").val(Detalles[0]);
    $("#hddIDProducto").val(Detalles[1]);
    $("#CodigoLote").val(Detalles[2]);
    $("#FechaRegistro").val(Detalles[3]);
    $("#FechaVencimiento").val(Detalles[4]);
    DefinirFecha("FechaRegistro");
    DefinirFecha("FechaVencimiento");
    
}

    function LimpiarCamposLote() {
        $("#hddIDLote").val(0);
        $("#hddIDProducto").val(0);
        $("#CodigoLote").val('');
        $("#FechaRegistro").val('');
        $("#FechaVencimiento").val('');

        $('.datepicker').datepicker({
            language: 'es',
            format: 'dd/mm/yyyy',
        });
    }

    function ValidarFormularioLote() {
        var Mensajes = '';
        if ($("#CodigoLote").val() == '' || $("#CodigoLote").val() == null) {
            Mensajes += 'Debe registrar un código de lote \n'
        }
        if ($("#FechaRegistro").val() == '' || $("#FechaRegistro").val() == null) {
            Mensajes += 'Debe seleccionar una fecha de registro \n'
        }
        if ($("#FechaVencimiento").val() == '' || $("#FechaVencimiento").val() == null) {
            Mensajes += 'Debe seleccionar una fecha de vencimiento \n'
        }

        return Mensajes;
    }

    function ObtenerDatosLote() {
        return $("#divFormularioLote :input").serialize();
    }


    function GuardarLote() {
        var Mensajes = ValidarFormularioLote();

        if (Mensajes == '') {
            LlamadoPost('/Lote/GuardarRegistro', ObtenerDatosLote());
        }
        else {MostrarMensaje(Mensajes);}
    }

    function EliminarLote(IdLote) {
        if (!ValidarAccion('/Lote/Eliminar', 'IdLote=' + IdLote))
        { LlamadoPost('/Lote/Eliminar', 'IdLote=' + IdLote); }
        else {
            MostrarMensaje('No se puede eliminar el lote porque ya se encuentra registrado en los inventarios');
        }
    }