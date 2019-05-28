using com.co.uan.DMiravalle.Administracion;
using System;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class LoginController : Controladores
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult validaUsuario()
        {    String Mensaje="";
            try
            {
                UsuarioDTO objUsuario = ObtenerNegocio().ObtenerFachadaAdministrativa().validarUsuario(Request["Correo"], Request["Clave"]);
                if (objUsuario != null)
                {
                    Session["Autenticado"] = objUsuario;
                    Resultado = true;
                }
                else
                {
                    Mensaje = "Error de acceso, por favor valide su usurio y/o contraseña";
                }
            }
            catch (Exception ex)
            {
                RegistarError(ex);
                Mensaje = "Se presento inconveniente al realizar la accion";
            }
            return Json(new { success = true, data = Resultado, mensaje = Mensaje });
        }
    }
}