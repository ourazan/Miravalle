using System;
using System.Linq;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class InventarioController : Controladores
    {
        // GET: Inventario
        public ActionResult Index()
        {
            ViewData["Inventario"] = ObtenerNegocio().ObtenerFachadaInventario().ConsultarInventario(" IdLote=" + Request["IdLote"]);
            return PartialView();
        }
        [HttpGet]
        public ActionResult Edicion()
        {
            SelectList Sedes;
            var Items = (from sede in ObtenerNegocio().ObtenerFachadaAdministrativa().ConsultarSede(" 1=1 ")
                         select new SelectListItem()
                         {
                             Text = sede.NombreSede,
                             Value = sede.IdSede.ToString()
                         });
            Sedes = new SelectList(Items, "Value", "Text");
            ViewData["Sede"] = Sedes;
            return PartialView();
        }
        [HttpPost]
        public ActionResult GuardarRegistro()
        {
            string Mensaje = "";
            bool Resultado = false;

            try
            {
                if (Request["hddIDInventario"] == "0")
                {
                   return Json (ObtenerNegocio().ObtenerFachadaInventario().CrearInventario(
                        Convert.ToInt32(Request["hddIDLote"])
                        ,Convert.ToInt32(Request["SedeInventario"])
                        ,Convert.ToInt32 (Request["Cantidad"])
                        ,Convert.ToDateTime( Request["FechaRegistroInventario"])));
                }
                else
                {
                    return Json(ObtenerNegocio().ObtenerFachadaInventario().ModificarInventario(
                        Convert.ToInt32(Request["hddIDLote"])
                        ,Convert.ToInt32(Request["SedeInventario"])
                        ,Convert.ToInt32(Request["Cantidad"])
                        ,Convert.ToInt32(Request["hddIDInventario"])
                        ,Convert.ToDateTime(Request["FechaRegistroInventario"])));
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
                    Resultado = ObtenerNegocio().ObtenerFachadaInventario().EliminarInventario(Convert.ToInt32(Request["IdInventario"]));
                    Mensaje = "No se pudo eliminar el inventario ";
                    if (Resultado)
                        Mensaje = "Se ha eliminado el inventario exitosamente";
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