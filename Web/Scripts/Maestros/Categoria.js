function AbrirModalNuevo(accion) {
    $("#divEdicionCategoria").modal('show');

    if (accion == '0') {
        //Para Crear
        LLimpiarCampos();
        $("#hddID").val(accion);
        $("#DivConsecutivo").attr("class", "hidden");

    } else {
        //Para editar
        $("#DivConsecutivo").attr("class","form-group row");
    }
    $('#ddlCategoria').change(function () {
        HabilitarCategoria();
    })
    HabilitarCategoria();

    PermitirNumero("txtConsecutivo");
    PermitirNumero("txtCodigo");
    return false;
}

function Guardar() {
    var Mensajes = ValidarFormulario();

    if (Mensajes == '') {
        LlamadoPost('/Categoria/Guardar', ObtenerDatos());
    }
    else { MostrarMensaje(Mensajes); }
}

function CargarResultados(Resultado) {

  MostrarMensaje(Resultado.mensaje);
  if (Resultado.data > 0) {
      
        LLimpiarCampos();
        $("#divEdicionCategoria").modal('hide');
        window.location.href = '/Categoria/Index';
    }
}

function ObtenerDatos() {
    return $("#divFormulario :input").serialize() + '&hddID=' + $("#hddID").val();

}

function CargarFormularioEdicion(categoria) {
    var arreglo = categoria.toString().split('-');
    $("#hddID").val(arreglo[0]);
    $("#txtNombre").val(arreglo[1]);
    $("#txtPrefijo").val(arreglo[2]);
    $("#txtConsecutivo").val(arreglo[3]);
    if ($("#txtConsecutivo").val()=='') {
       $("#txtConsecutivo").val('0');
    }
    $("#txtCodigo").val(arreglo[4]);
    $("#ddlCategoria").val(arreglo[5]);
    AbrirModalNuevo(arreglo[0]);
}

function HabilitarCategoria() {
    
    if ($('#ddlCategoria').val() != "0")// Para subcategorias
    {
        $("#DivConsecutivo").attr("class", "hidden");
        $("#DivCodigo").attr("class", "hidden");
        $("#DivPrefijo").attr("class", "hidden");
        $("#txtPrefijo").val('');
        $("#txtConsecutivo").val('0');
        $("#txtCodigo").val('0');
        $("#exampleModalLabel").text('Subcategoría'); 
    }
    else {
        if($("#hddID").val()=='0'){
            $("#DivConsecutivo").attr("class", "hidden");
            $("#txtConsecutivo").val('0');
        }
        $("#DivCodigo").attr("class", "form-group row");
        $("#DivPrefijo").attr("class", "form-group row");
        $("#exampleModalLabel").text('Categoría');
    }

}

function LLimpiarCampos() {
    $("#hddID").val(0);
    $("#txtNombre").val('');
    $("#txtPrefijo").val('');
    $("#txtConsecutivo").val('');
    $("#txtCodigo").val('');
    $("#ddlCategoria").val('0');
    $("#DivCodigo").attr("class", "form-group row");
    $("#DivPrefijo").attr("class", "form-group row");
    $("#exampleModalLabel").text('Categoría');

}

function ValidarFormulario() {
    var Mensajes = '';
    if ($("#txtNombre").val() == '' || $("#txtNombre").val() == null) {
        Mensajes+='Debe seleccionar un nombre \n'
    }
    if (($("#txtPrefijo").val() == '' || $("#txtPrefijo").val() == null)&& $('#ddlCategoria').val() == "0") {
        Mensajes += 'Debe registrar un prefijo \n'
    }
    if (($("#txtConsecutivo").val() == '' || $("#txtConsecutivo").val() == null) && $("#DivConsecutivo").prop("class") != "hidden" && $('#ddlCategoria').val() == "0") {
        Mensajes += 'Debe registrar un consecutivo \n'
    }
    if (($("#txtCodigo").val() == '' || $("#txtCodigo").val() == null) && $('#ddlCategoria').val() == "0") {
        Mensajes += 'Debe registrar un código \n'
    }
    if (($("#txtCodigo").val() != '' || $("#txtCodigo").val() != null) && $('#ddlCategoria').val() == "0" && $("#txtCodigo").val().length>2) {
        Mensajes += 'El código solo debe tener dos digitos \n'
    }




    return Mensajes;
}

function CategoriaNuevo() {
    AbrirModalNuevo('0');
}
$(function () {
    var Adicion = $('<a href="#" class="ace-icon fa fa-plus"  title="Crear nuevo registro" onclick="CategoriaNuevo();"  ></a>');
    //Append it to the First cell of Header Row.
    $("#grdCategoria th:first-child").append(Adicion);
});