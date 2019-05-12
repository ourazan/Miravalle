using System;
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
                    ViewData["Producto"] = ObtenerNegocio().ObtenerFachadaInventario().ConsultarProducto("1=1");
                     var Items= (from Tipo in ObtenerNegocio().ObtenerFachadaInventario().ConsultarTipoProducto("1=1")
                                               select new SelectListItem() {
                                                   Text=Tipo.Descripcion,
                                                   Value=Tipo.IdTipoProducto.ToString()
                                               } 
                                             );
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
                    Resultado = ObtenerNegocio().ObtenerFachadaInventario().CrearProducto(Request["NombreProducto"],Convert.ToInt32( Request["TipoProducto"]));
                    Mensaje = "No se pudo crear el producto";
                    if (Resultado)
                        Mensaje = "Se ha creado el producto exitosamente";

                }
                else
                {
                    Resultado = ObtenerNegocio().ObtenerFachadaInventario().EditarProducto(Request["NombreProducto"], Convert.ToInt32(Request["TipoProducto"]),Convert.ToInt32(Request["hddID"]));
                    Mensaje = "No se pudo editar el producto ";
                    if (Resultado )
                        Mensaje = "Se ha editado el producto exitosamente";
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

        [HttpPost]
        public ActionResult EliminarRegistro()
        {
            try
            {
                if (ObtenerNegocio().ObtenerFachadaInventario().ConsultarInventario("IdProducto=" + Request["hddID"]).Count == 0) {

                    Resultado = ObtenerNegocio().ObtenerFachadaInventario().EliminarProducto(Convert.ToInt32(Request["hddID"]));
                    Mensaje = "No se pudo eliminar el producto ";
                    if (Resultado)
                        Mensaje = "Se ha eliminado el producto exitosamente";

                } else {
                    Resultado = false;
                    Mensaje  = "No se puede eliminar el producto porque ya se encuentra registrado en los inventarios";
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