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
                    Mensaje = "No se pudo crear el Lote ";
                    if (Resultado)
                        Mensaje = "Se ha creado el lote exitosamente";

                }
                else
                {
                    Resultado = ObtenerNegocio().ObtenerFachadaInventario().EditarLote(Request["CodigoLote"]
                        , Convert.ToDateTime(Request["FechaVencimiento"])
                        , Convert.ToInt32(Request["hddIDProducto"])
                        , Convert.ToInt32(Request["hddIDLote"])
                        , Convert.ToDateTime(Request["FechaRegistro"])
                        );
                    Mensaje = "No se pudo editar el lote ";
                    if (Resultado)
                        Mensaje = "Se ha editado el lote exitosamente";
                }
            }
            catch (Exception ex)
            {
                RegistarError(ex);
                Resultado = false;
                Mensaje = "Se presento inconveniente al realizar la acción";
            }
            return Json(new { success = true, data = Resultado, mensaje = Mensaje });
        }
        [HttpPost]
        public ActionResult Eliminar()
        {
            try
            {
                if (ObtenerNegocio().ObtenerFachadaInventario().ConsultarInventario(Convert.ToInt32 ( Request["IdLote"]),0,-1,0,null,0).Count == 0)
                {

                    Resultado = ObtenerNegocio().ObtenerFachadaInventario().EliminarLote(Convert.ToInt32(Request["IdLote"]));
                    Mensaje = "No se pudo eliminar el lote ";
                    if (Resultado)
                        Mensaje = "Se ha eliminado el lote exitosamente";

                }
                else
                {
                    Resultado = false;
                    Mensaje = "No se puede eliminar el lote porque ya se encuentra registrado en los inventarios";
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