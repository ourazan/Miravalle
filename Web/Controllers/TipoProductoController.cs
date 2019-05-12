using System;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class TipoProductoController : Controladores
    {
        // GET: TipoProducto
        public ActionResult Index()
        {
            try
            {
            ViewData["TiposProducto"] = ObtenerNegocio().ObtenerFachadaInventario().ConsultarTipoProducto("activo=1");
            ViewData["Autenticado"] = ObtenerAutenticado();
            return View();

            }
            catch (Exception ex)
            {

                RegistarError(ex);
                throw;
            }
        }

        [HttpPost]
        public ActionResult GuardarRegistro()
        {
            try
            {
                Mensaje = "";
                if (Request["hddID"].Equals("0"))
                {
                    Resultado = ObtenerNegocio().ObtenerFachadaInventario().ExisteCodigoReferencia(Request["CodigoReferencia"], 0);
                    if (!Resultado)
                    {
                        Mensaje = "No Se creo el tipo de producto";
                        Resultado = ObtenerNegocio().ObtenerFachadaInventario().CrearTipoProducto(
                            Request["Descripcion"]
                            , Request["CodigoReferencia"]);
                        if (Resultado)
                            Mensaje = "Se creo el tipo de producto exitosamente";
                    }
                    else
                    {
                        Resultado = false;
                        Mensaje = "Ya se encuentra el codigo de referencia " + Request["CodigoReferencia"] + " en el sistema";
                    }

                }
                else
                {

                    Resultado = ObtenerNegocio().ObtenerFachadaInventario().ExisteCodigoReferencia(Request["CodigoReferencia"], Convert.ToInt32(Request["hddID"]));
                    if (!Resultado)
                    {
                        Mensaje = "No se creo el tipo de producto";
                        Resultado = ObtenerNegocio().ObtenerFachadaInventario().EditarTipoProducto(
                            Request["Descripcion"]
                            , Request["CodigoReferencia"]
                            , Convert.ToInt32(Request["hddID"]));
                        if (Resultado)
                            Mensaje = "Se editó el tipo de producto exitosamente";
                    }
                    else
                    {
                        Resultado = false;
                        Mensaje = "Ya se encuentra el codigo de referencia " + Request["CodigoReferencia"] + " en el sistema";
                    }


                }
            }
            catch (Exception ex)
            {
                RegistarError(ex);
                Resultado = false;
                Mensaje = "No se puedo guardar los cambios para el producto";
            }

            return Json(new { success = true, data = Resultado, mensaje = Mensaje });
        }

    }
}