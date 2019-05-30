$(document).ready(function () {
//MANEJO DE CONTROLES
$("#BtnLogin").click(function () {
    var input = $('.validate-input .input100');

    if (Recorre(input) === true) {
        login();
    }

});
  

});



//FUNCIONES
function Recorre(input) {
    var check = true;
    for (var i = 0; i < input.length; i++) {
        if (validate(input[i]) === false) {
            showValidate(input[i]);
            check = false;
        } else {
            hideValidate(input[i]);
        }
    }
    return check;
}

function validate(input) {

    if ($(input).attr('type') === 'correo' || $(input).attr('name') === 'correo') {

        var text = $(input).val().trim().match(/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{1,5}|[0-9]{1,3})(\]?)$/);
        var login = $('#correo').val();

        if (login === '') {
            if (text === null) {
                return false;
            }
        }
    } else {
        if ($(input).val().trim() === '') {
            return false;
        }
    }

}

function showValidate(input) {
    var thisAlert = $(input).parent();

    $(thisAlert).addClass('alert-validate');
}

function hideValidate(input) {
    var thisAlert = $(input).parent();

    $(thisAlert).removeClass('alert-validate');
}

function login() {
    LlamadoPost('/Login/validaUsuario', ObtenerDatos());
}

function ObtenerDatos() {
    return $("#divFormulario :input").serialize();
}

function CargarResultados(Resultado, ur) {
       
        if (Resultado.data) {
                  window.location.href = '/Home/Index';
        } else {
            MostrarMensaje(Resultado.mensaje);
        }
    }
