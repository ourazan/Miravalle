




function LlamadoPostXMLHttp(url, data) {
    var xhr = new XMLHttpRequest();
    xhr.open('POST', url);
    xhr.send(data);
    xhr.onreadystatechange = function () {
       
            if (xhr.readyState == 4 && xhr.status == 200) {
                CargarResultados(JSON.parse(xhr.response));
            }
        
    }
}


function LlamadoPost(url, data) {
    $.post(url
        , data
        , function (data, status) {
            CargarResultados(data);
         });

}

function MostrarMensaje(texto) {
    alert(texto);
}

$(document).ready(function () {
    $('.datepicker').datepicker({
        language: 'es',
        format: 'dd/mm/yy',
    });
});

try { ace.settings.check('sidebar', 'fixed') } catch (e) { }
try { ace.settings.check('sidebar', 'collapsed') } catch (e) { }



function FiltroTexto(Expresion, Control) {
    $("#"+Control).on("keypress keyup blur", function (event) {
        $(this).val($(this).val().replace(Expresion, ""));
        if ((event.which < 48 || event.which > 57)) {
            event.preventDefault();
        }
    });
}

function PermitirNumero(Control) {
    FiltroTexto(/[^\d].+/,Control);
}

function CargarProceso() {
               var imagen = ' <img src="/Content/Images/Carga.gif" />';
               $('#btnGuardar').append(imagen);
               $('#btnGuardar').attr('Disabled','true');
}

function CerrarProceso() {
    if($('#btnGuardar') != null)
    {
        $('#btnGuardar').find('img').remove();
        $('#btnGuardar').removeAttr('Disabled');
    }
}
