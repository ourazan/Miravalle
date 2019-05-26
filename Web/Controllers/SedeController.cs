using System;
using System.Linq;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class SedeController : Controladores
    {
        // GET: Sede
        public ActionResult Index()
        {
            ViewData["Autenticado"] = ObtenerAutenticado();
            SelectList Administradores;
            var Items = (from Administrador in ObtenerNegocio().ObtenerFachadaAdministrativa().ConsultarUsuario(0,string.Empty, string.Empty,0, string.Empty,0)
                         select new SelectListItem()
                         {
                             Text = Administrador.Nombre +" " + Administrador.Apellido,
                             Value = Administrador.IdUsuario.ToString()
                         } ).ToList();

            Items= AdicionarValorDefecto(Items);
            Administradores = new SelectList(Items, "Value", "Text");
            ViewData["Administradores"] = Administradores;
            ViewData["Sede"] = ObtenerNegocio().ObtenerFachadaAdministrativa().ConsultarSede(string.Empty, string.Empty, string.Empty, 0,0);
            return View();
        }

        [HttpPost]
        public ActionResult GuardarRegistro()
        {
            try
            {
                if (Request["hddID"] == "0")
                {
                    Resultado = ObtenerNegocio().ObtenerFachadaAdministrativa().CrearSede(Request["NombreSede"]
                        , Request["Ciudad"],  Request["Direccion"]
                        , ( string.IsNullOrEmpty( Request["Administrador"])?0:Convert.ToInt32(Request["Administrador"])));
                }
                else
                {
                    Resultado = ObtenerNegocio().ObtenerFachadaAdministrativa().EditarSede(Request["NombreSede"]
                        , Request["Ciudad"]
                        , Request["Direccion"]
                        , (string.IsNullOrEmpty(Request["Administrador"])? 0 : Convert.ToInt32(Request["Administrador"]))
                        ,Convert.ToInt32(Request["hddID"]) );
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
        public ActionResult EliminarRegistro()
        {
            try
            {
               Resultado = ObtenerNegocio().ObtenerFachadaAdministrativa().EliminarSede(Convert.ToInt32(Request["hddID"]));
            }
            catch (Exception ex)
            {
                RegistarError(ex);
                Resultado = false;
            }
            return Json(new { success = true, data = Resultado});
        }

        [HttpPost]
        public ActionResult ExisteSedeXInventario()
        {
            try
            {
                Resultado = ObtenerNegocio().ObtenerFachadaInventario().ConsultarInventario(0, Convert.ToInt32(Request["hddID"]), -1, 0, null, 0).Count > 0;
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