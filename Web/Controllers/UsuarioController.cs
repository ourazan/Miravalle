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
            List<SelectListItem> Items = (from sede in ObtenerNegocio().ObtenerFachadaAdministrativa().ConsultarSede(string.Empty,string.Empty,string.Empty,0,0)
                         select new SelectListItem()
                         {
                             Text = sede.NombreSede,
                             Value = sede.IdSede.ToString()
                         }).ToList();

            Items = AdicionarValorDefecto(Items);
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
            PerfilOpciones = AdicionarValorDefecto(PerfilOpciones);
            SelectList Perfiles = new SelectList(PerfilOpciones, "Value", "Text");
            ViewData["Perfiles"] = Perfiles;
            ViewData["Usuario"] = ObtenerNegocio().ObtenerFachadaAdministrativa().ConsultarUsuario(0,string.Empty,string.Empty,0,string.Empty,0);
            return View();
        }

        [HttpPost]
        public ActionResult GuardarRegistro()
        {
            
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
                  
                }
            }
            catch (Exception ex)
            {
                RegistarError(ex);
                Resultado = false;
            }
            return Json(new { success = true, data = Resultado });
        }

        [HttpPost]
        public ActionResult Eliminar()
        {
            try
            {
                Resultado = ObtenerNegocio().ObtenerFachadaAdministrativa().RemoverUsuario(Convert.ToInt32(Request["hddID"]));
                    if (Convert.ToInt32(Request["hddID"])== ObtenerUsuarioAutenticado())
                        CerrarSesion();
            }
            catch (Exception ex)
            {
                RegistarError(ex);
                Resultado = false;
            }
            return Json(new { success = true, data = Resultado});
        }

        [HttpPost]
        public ActionResult ExisteUsuario()
        {
            try
            {
                Resultado = ObtenerNegocio().ObtenerFachadaAdministrativa().RemoverUsuario(Convert.ToInt32(Request["hddID"]));
                if (Convert.ToInt32(Request["hddID"]) == ObtenerUsuarioAutenticado())
                    CerrarSesion();
            }
            catch (Exception ex)
            {
                RegistarError(ex);
                Resultado = false;
            }
            return Json(new { success = true, data = Resultado });
        }
    }
}