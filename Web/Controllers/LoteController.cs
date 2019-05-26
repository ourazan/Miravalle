using System;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class LoteController : Controladores
    {
        [HttpGet]
        public ActionResult ConsultaLotes()
        {
            ViewData["Autenticado"] = ObtenerAutenticado();
            ViewData["Lote"] = ObtenerNegocio().ObtenerFachadaInventario().ConsultarLote(string.Empty,null,Convert.ToInt32( Request["IdProducto"]),0,null);
            return PartialView();
        }
        [HttpGet]
        public ActionResult Edicion()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult GuardarRegistro()
        {
            try
            {
                if (Request["hddIDLote"] == "0")
                {
                    Resultado = ObtenerNegocio().ObtenerFachadaInventario().CrearLote(Request["CodigoLote"]
                        , Convert.ToDateTime(Request["FechaVencimiento"])
                        , Convert.ToInt32(Request["hddIDProducto"])
                        , Convert.ToDateTime(Request["FechaRegistro"])
                        );
                }
                else
                {
                    Resultado = ObtenerNegocio().ObtenerFachadaInventario().EditarLote(Request["CodigoLote"]
                        , Convert.ToDateTime(Request["FechaVencimiento"])
                        , Convert.ToInt32(Request["hddIDProducto"])
                        , Convert.ToInt32(Request["hddIDLote"])
                        , Convert.ToDateTime(Request["FechaRegistro"])
                        );
                 
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
                    Resultado = ObtenerNegocio().ObtenerFachadaInventario().EliminarLote(Convert.ToInt32(Request["IdLote"]));
            }
            catch (Exception ex)
            {
                RegistarError(ex);
                Resultado = false;
            }
            return Json(new { success = true, data = Resultado });

        }
        [HttpPost]
        public ActionResult ExisteLoteXInventario()
        {
            try
            {
                Resultado= ObtenerNegocio().ObtenerFachadaInventario().ConsultarInventario(Convert.ToInt32(Request["IdLote"]), 0, -1, 0, null, 0).Count>0;
            }
            catch (Exception ex )
            {
                RegistarError(ex);
                Resultado = false;
            }
            return Json(new { success = true, data = Resultado });
        }
      }
}