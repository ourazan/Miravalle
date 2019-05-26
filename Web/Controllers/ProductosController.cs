using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ProductosController : Controladores
    {
        // GET: Productos
        public ActionResult Index()
        {
            try
            {
                ViewData["Autenticado"] = ObtenerAutenticado();
                SelectList Tipos;
                ViewData["Producto"] = ObtenerNegocio().ObtenerFachadaInventario().ConsultarProducto( 
                    string.IsNullOrEmpty( Request["Nombre"])?string.Empty  :Request["Nombre"]
                    ,string.IsNullOrEmpty(Request["TipoProducto"]) ?0:Convert.ToInt32(Request["TipoProducto"])
                    , 0);
              var  Items = (from Tipo in ObtenerNegocio().ObtenerFachadaInventario().ConsultarTipoProducto(string.Empty, string.Empty, 0)
                             select new SelectListItem()
                             {
                                 Text = Tipo.Descripcion,
                                 Value = Tipo.IdTipoProducto.ToString()
                             } ).ToList();
                Items= AdicionarValorDefecto(Items);
                Tipos = new SelectList(Items, "Value", "Text");
                ViewData["TiposProducto"] = Tipos;
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
                if (Request["hddID"] == "0")
                {
                    Resultado = ObtenerNegocio().ObtenerFachadaInventario().CrearProducto(Request["NombreProducto"], Convert.ToInt32(Request["TipoProducto"]));
                }
                else
                {
                    Resultado = ObtenerNegocio().ObtenerFachadaInventario().EditarProducto(Request["NombreProducto"], Convert.ToInt32(Request["TipoProducto"]), Convert.ToInt32(Request["hddID"]));
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
               Resultado = ObtenerNegocio().ObtenerFachadaInventario().EliminarProducto(Convert.ToInt32(Request["hddID"]));
            }
            catch (Exception ex)
            {
                RegistarError(ex);
                Resultado = false;
            }
            return Json(new { success = true, data = Resultado });
        }
        [HttpPost]
        public ActionResult ExisteProductoEnInventario()
        {
            try
            {
                Resultado= ObtenerNegocio().ObtenerFachadaInventario().ConsultarInventario(0, 0, 0, 0, null, Convert.ToInt32(Request["hddID"])).Count > 0;
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