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
            ViewData["TiposProducto"] = ObtenerNegocio().ObtenerFachadaInventario().ConsultarTipoProducto(string.Empty,string.Empty,0);
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
               
                if (Request["hddID"].Equals("0"))
                {                
                    Resultado = ObtenerNegocio().ObtenerFachadaInventario().CrearTipoProducto(
                        Request["Descripcion"]
                        , Request["CodigoReferencia"]);
                  
                }
                else
                {
                    Resultado = ObtenerNegocio().ObtenerFachadaInventario().EditarTipoProducto(
                        Request["Descripcion"]
                        , Request["CodigoReferencia"]
                        , Convert.ToInt32(Request["hddID"]));
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
                    Resultado = ObtenerNegocio().ObtenerFachadaInventario().EliminarTipoProducto(Convert.ToInt32(Request["hddID"]));
            }
            catch (Exception ex)
            {
                RegistarError(ex);
                Resultado = false;
            }
            return Json(new { success = true, data = Resultado });
        }


        [HttpPost]
        public ActionResult ExisteCodigoReferencia()
        {
            try
            {
                Resultado = ObtenerNegocio().ObtenerFachadaInventario().ExisteCodigoReferencia(Request["CodigoReferencia"], Convert.ToInt32(Request["hddID"]));
            }
            catch (Exception ex)
            {
                RegistarError(ex);
                Resultado = false;
            }
            return Json(new { success = true, data = Resultado });
        }

        [HttpPost]
        public ActionResult ExisteProductosXTipo()
        {
            try
            {
                Resultado = ObtenerNegocio().ObtenerFachadaInventario().ConsultarProducto(string.Empty, Convert.ToInt32(Request["hddID"]), 0).Count >0;
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