using System.Web.Mvc;
using System;

namespace Web.Controllers
{
    public class UsuarioController : Controladores
    {
       
        public ActionResult Index()
        {
            ViewData["Autenticado"] = ObtenerAutenticado();
            return View();
        }

        [HttpPost]
        public ActionResult GuardarRegisro()
        {
            string Mensaje = "";
            Boolean Resultado = false;
            try
            {
                if (Request["id"] == "0")
                {
                    Resultado = ObtenerNegocio().ObtenerFachadaAdministrativa().CrearUsuario
                        (Request["txtNombres"]
                        , Request["txtApellidos"]
                        , Convert.ToInt32(Request["ddlSede"])
                        , Request["txtCorreo"]
                        , Request["txtUsuario"]
                        , Request["txtClave"]);
                        Mensaje = "No se pudo crear el usuario ";
                    if (Resultado)
                        Mensaje = "Se ha creado el usuario exitosamente";
                }
                else
                {
                    Resultado = ObtenerNegocio().ObtenerFachadaAdministrativa().EditarUsuario(
                        Convert.ToInt32(Request["id"])
                        , Request["txtNombres"]
                        , Request["txtApellidos"]
                        , Convert.ToInt32(Request["ddlSede"])
                        , Request["txtCorreo"]
                        , Request["txtClave"]
                        );
                        Mensaje = "No se pudo editar el usuario ";
                    if (Resultado)
                        Mensaje = "Se ha editado el usuario exitosamente";
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