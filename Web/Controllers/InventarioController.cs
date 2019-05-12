using System;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class InventarioController : Controladores
    {
        // GET: Inventario
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GuardarRegisro()
        {
            string Mensaje = "";
            bool Resultado = false;

            try
            {
                if (Request["id"] == "0")
                {
                    Resultado = ObtenerNegocio().ObtenerFachadaInventario().CrearInventario(
                        Convert.ToInt32(Request["IdLote"])
                        , Convert.ToInt32(Request["IdSede"])
                        , Convert.ToInt32 (Request["txtCantidad"])
                        ,Convert.ToDateTime( Request["FechaRegistro"]));
                      Mensaje = "No se pudo crear el inventario ";
                    if (Resultado)
                        Mensaje = "Se ha creado el inventario exitosamente";

                }
                else
                {
                    Resultado = ObtenerNegocio().ObtenerFachadaInventario().EditarInventario(
                        Convert.ToInt32(Request["IdLote"])
                        , Convert.ToInt32(Request["IdSede"])
                        , Convert.ToInt32(Request["txtCantidad"])
                        , Convert.ToInt32(Request["id"])
                        , Convert.ToDateTime(Request["FechaRegistro"]));
                 
                    Mensaje = "No se pudo editar el inventario ";
                    if (Resultado )
                        Mensaje = "Se ha editado el inventario exitosamente";
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