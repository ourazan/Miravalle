using System.Web.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Web.Controllers
{
    public class UsuarioController : Controladores
    {
       
        public ActionResult Index()
        {
            ViewData["Autenticado"] = ObtenerAutenticado();
            SelectList Sedes;
            var Items = (from sede in ObtenerNegocio().ObtenerFachadaAdministrativa().ConsultarSede(string.Empty,string.Empty,string.Empty,0,0)
                         select new SelectListItem()
                         {
                             Text = sede.NombreSede,
                             Value = sede.IdSede.ToString()
                         });
            Sedes = new SelectList(Items, "Value", "Text");
            ViewData["Sedes"] = Sedes;

            List<SelectListItem> PerfilOpciones = new List<SelectListItem>() {
               new SelectListItem(){
                             Text = "Gerente",
                             Value = "1"},
                new SelectListItem(){
                             Text = "Administrador Sede",
                             Value = "2"}
            };
            SelectList Perfiles = new SelectList(PerfilOpciones, "Value", "Text");
            ViewData["Perfiles"] = Perfiles;
            ViewData["Usuario"] = ObtenerNegocio().ObtenerFachadaAdministrativa().ConsultarUsuario(0,string.Empty,string.Empty,0,string.Empty,0);
            return View();
        }

        [HttpPost]
        public ActionResult GuardarRegistro()
        {
            string Mensaje = "";
            Boolean Resultado = false;
            try
            {
                if (Request["hddID"] == "0")
                {
                    Resultado = ObtenerNegocio().ObtenerFachadaAdministrativa().CrearUsuario
                        (Request["Nombre"]
                        , Request["Apellido"]
                        , Request["Sede"]==null?0:  Convert.ToInt32(Request["Sede"])
                        , Request["Correo"]
                        , Request["Usuario"]
                        , Request["Clave"]
                        ,Convert.ToInt32(Request["Perfil"]));
                        Mensaje = "No se pudo crear el usuario ";
                    if (Resultado)
                        Mensaje = "Se ha creado el usuario exitosamente";
                }
                else
                {
                    Resultado = ObtenerNegocio().ObtenerFachadaAdministrativa().EditarUsuario(
                        Convert.ToInt32(Request["hddID"])
                        , Request["Nombre"]
                        , Request["Apellido"]
                        , Request["Sede"] == null ? 0 : Convert.ToInt32(Request["Sede"])
                        , Request["Correo"]
                        , ""
                        , Convert.ToInt32(Request["Perfil"]));
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

        [HttpPost]
        public ActionResult Eliminar()
        {
            try
            {
                Resultado = ObtenerNegocio().ObtenerFachadaAdministrativa().RemoverUsuario(Convert.ToInt32(Request["hddID"]));
                Mensaje = "No se pudo eliminar el usuario ";
                if (Resultado)
                {
                    Mensaje = "Se ha eliminado el usuario exitosamente";
                    if (Convert.ToInt32(Request["hddID"])== ObtenerUsuarioAutenticado())
                        CerrarSesion();
                }
            }
            catch (Exception ex)
            {
                RegistarError(ex);
                Resultado = false;
                Mensaje = "Se presento inconveniente al realizar la accion";
            }
            return Json(new { success = true, data = Resultado, mensaje = Mensaje });
        }
    }
}