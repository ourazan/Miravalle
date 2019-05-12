function AbrirModal(modal) {
    $("#" + modal).modal('show');
    return false;
}

function Guardar() {
    CargarProceso();
    var mensajes = ValidarCampos();

    if (mensajes == '') {
        LlamadoPostXMLHttp('/Consecutivos/Guardar', CargarAdjuntos());
    } else {
        MostrarMensaje(mensajes);
        CerrarProceso();
    }
}

function CargarResultados(Resultado) {

    MostrarMensaje(Resultado.mensaje);
    if (Resultado.data > 0) {
        $("#divNuevoConsecutivo").modal('hide');
        window.location.href = '/Consecutivos/Index';
        CerrarProceso();
    }
}


function ObtenerDatos() {
    var Campos = new FormData();
    for (var i = 0; i < $($("#divCreacion")[0]).find(':input').length; i++) {
        if ($($($("#divCreacion")[0]).find(':input')[i]).prop('type') != 'submit') {
            if ($($($("#divCreacion")[0]).find(':input')[i]).prop('id') == 'chkIndefinida') {
                Campos.append($($($("#divCreacion")[0]).find(':input')[i]).prop('id'), $("#chkIndefinida").is(":checked"));
            } else {
                Campos.append($($($("#divCreacion")[0]).find(':input')[i]).prop('id'), $($($("#divCreacion")[0]).find(':input')[i]).val());
            }
        }
    }

    return Campos;
}
function CargarFormularioDetalle(categoria) {
    var arreglo = categoria.toString().split('|');
    LLimpiarCampos();
    $("#txtConsecutivo").val(arreglo[0]);
    $("#txtUsuario").val(arreglo[1]);
    $("#txtFechaInsercion").val(arreglo[2]);
    $("#txtCategoria").val(arreglo[3]);
    $("#txtSubCategoria").val(arreglo[4]);
    $("#txtVentas").val(arreglo[5]);
    $("#txtInicia").val(arreglo[6]);
    $("#txtFina").val(arreglo[7]);
    $("#txtDescripciones").val(arreglo[8]);
    AbrirModal('divConsecutivos');
}


function LLimpiarCampos() {
    $("#txtConsecutivo").val('');
    $("#txtUsuario").val('');
    $("#txtFechaInsercion").val('');
    $("#txtCategoria").val('');
    $("#txtSubCategoria").val('');
    $("#txtVentas").val('');
    $("#txtDescripciones").val('');
    $("#txtInicia").val('');
    $("#txtFina").val('');
    $("#txtNombre").val('');

}

function ObtenerSubcategorias(IdCategoria,IdSubcategoria) {
    $('#' + IdSubcategoria).empty();

    if ($('#' + IdCategoria).val() != "") {
            $.ajax({
                url: "/Consecutivos/ConsultarSubcategorias",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify({ CategoriaID: $('#' + IdCategoria).val() }),
                success: function (response) {
                    if (response != null) {
                        if (response.length > 0) {
                            $('#' + IdSubcategoria).append("<option value=''>SELECCIONE</option>");
                            for (var i = 0; i < response.length; i++) {
                                var objeto = response[i];
                                $('#' + IdSubcategoria).append("<option value='" + objeto.CategoriaID + "'>" + objeto.Nombre + "</option>");
                            }
                        }
                        else {
                            alert("No existe subcategorias relacionadas");
                        }
                    }
                },
                error: function (xhr, status) {
                    alert('Error al consultar subcategorias ' + status);
                }
            });
    }
}


function CargarControlesEdicion() {
    $('.datepicker').datepicker({
        language: 'es',
        format: 'dd/mm/yyyy'
    });

    $('#ddlCategoria').change(function () {
        ObtenerSubcategorias('ddlCategoria', 'ddlSubcategoria');
    })

    $('#chkIndefinida').change(function () {
        if ($("#chkIndefinida").is(":checked")) {
            $("#divFinal").attr("class", "hidden");
            $("#txtFinal").attr("required", "false");
        } else {
            $("#divFinal").attr("class", "form-group row");
            $("#txtFinal").attr("required", "true");
        }
    })
    ObtenerSubcategorias('ddlCategoria', 'ddlSubcategoria');

}

function ValidarCampos() {
    var mensaje = '';

    if ($("#ddlCategoria").val() == '' || $("#ddlCategoria").val() == null)
    { mensaje += 'Es obligatorio seleccionar una categoria \n'; }
    if ($("#ddlSubcategoria").val() == '' || $("#ddlSubcategoria").val() == null)
    { mensaje += 'Es obligatorio seleccionar una subcategoria \n'; }
    if ($("#txtInicial").val() == '' || $("#txtInicial").val() == null)
    { mensaje += 'Es obligatorio seleccionar una fecha inicial \n'; }
    if(!$("#chkIndefinida").is(":checked"))
    {
        if (($("#txtFinal").val() == '' || $("#txtFinal").val() == null))
        { mensaje += 'Es obligatorio seleccionar una fecha final \n'; }
        else {
            var ArrIni = $("#txtInicial").val().split("/");
            var ArrFin = $("#txtFinal").val().split("/");

            if (new Date(ArrIni[2], ArrIni[1], ArrIni[0]).getTime() > new Date(ArrFin[2], ArrFin[1], ArrFin[0]).getTime()) {
               mensaje += 'La fecha inicial no puede ser mayor a la fecha final \n';
        
            }

        }
     
    }
    if ($("#ddlVentas").val() == '' || $("#ddlVentas").val() == null)
    { mensaje += 'Es obligatorio diligenciar una opcion de ventas \n'; }
    if ($("#txtDescripcion").val() == '' || $("#txtDescripcion").val() == null)
    { mensaje += 'Es obligatorio diligenciar una descripcion \n'; }

    if ($("#flpAnexos")[0].files.length == 0) {
        mensaje += 'Es obligatorio agregar al menos 1 archivo  \n';
    } else {
        mensaje += ValidarTamanoArchivos();
    }
 
    return mensaje;
}

function CargarAdjuntos() {
    var Campos =ObtenerDatos();
    for (i = 0; i < $("#flpAnexos")[0].files.length; i++) {
        Campos.append($("#flpAnexos")[0].files[i].name, $("#flpAnexos")[0].files[i]);
    }
    return Campos;
}


function ValidarTamanoArchivos() {
    var tamanio = 0;
    var Mensaje = "";
    for (i = 0; i < $("#flpAnexos")[0].files.length; i++) {
        tamanio+=$("#flpAnexos")[0].files[i].size;
    }
    if (tamanio > 10485760)
    { Mensaje = "El tamaño total de los archivos no pueden superara los 10 Mb \n"; }
    return Mensaje ;
}




function CargarAnexos(CaveID, Consecutivo) {

    $.ajax({
        url: "/Consecutivos/ConsultarAnexos",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ ClaveID: CaveID }),
        success: function (response) {
            if (response != null) {
                if (response.length > 0) {

                    $('#DivFormularioAnexos').empty();
                    $('#TituloAnexo').empty();
                    for (var i = 0; i < response.length; i++) {
                        var objeto = response[i];
                        var textoFuncion = "DescargarAnexo('" + objeto.Ruta.replace('~', '') + "')";
                        $('#DivFormularioAnexos').append('<a href="#" onclick="' + textoFuncion + '"  >' + objeto.Ruta.split('/')[objeto.Ruta.split('/').length - 1] + '</a></br>');
                    }
                    $('#TituloAnexo').append('Anexos de clave de liberación: ' + Consecutivo);
                    AbrirModal('divDetalleAnexos');

                }
                else {
                    MostrarMensaje("No existe anexos relacionadas");
                }
            }
        },
        error: function (xhr, status) {
            MostrarMensaje('Error al consultar anexos ' + status);
        }
    });


}

function DescargarAnexo(Ruta) {
    window.open("/Consecutivos/ObtenerAnexo?Ruta=" + Ruta);
}


$(function () {
    var Adicion = $('<a href="#" class="ace-icon fa fa-plus" title="Crear nuevo registro" onclick="CargarFormularioNuevo();"  ></a>');
    $("#grdConsecutivos th:first-child").append(Adicion);
    $('#ddlCategoriaBUS').change(function () {
        ObtenerSubcategorias('ddlCategoriaBUS', 'ddlSubcategoriaBUS');
    })
    }
);

