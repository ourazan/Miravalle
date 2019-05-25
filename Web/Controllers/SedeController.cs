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
                         }
                                    );
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
                        , (Request["Administrador"]==null?0:Convert.ToInt32(Request["Administrador"])));
                    Mensaje = "No se pudo crear la sede ";

                    if (Resultado)
                    {
                        Mensaje = "Se ha creado la sede exitosamente";
                    }

                }
                else
                {
                    Resultado = ObtenerNegocio().ObtenerFachadaAdministrativa().EditarSede(Request["NombreSede"]
                        , Request["Ciudad"]
                        , Request["Direccion"]
                        , (Request["Administrador"] == null || Request["Administrador"] ==""? 0 : Convert.ToInt32(Request["Administrador"]))
                        ,Convert.ToInt32(Request["hddID"]) );
                    Mensaje = "No se pudo editar la sede ";
                    if (Resultado )
                        Mensaje = "Se ha editado la sede exitosamente";
                }

            }
            catch (Exception ex)
            {
                Mensaje = "Se presento inconveniente al realizar la accion";
            }

            return Json(new { success = true, data = Resultado, mensaje = Mensaje });

        }

        [HttpPost]
        public ActionResult EliminarRegistro()
        {
            try
            {
                if (ObtenerNegocio().ObtenerFachadaInventario().ConsultarProducto(string .Empty,0,Convert.ToInt32 ( Request["hddID"])).Count == 0)
                {

                    Resultado = ObtenerNegocio().ObtenerFachadaAdministrativa().EliminarSede(Convert.ToInt32(Request["hddID"]));
                    Mensaje = "No se pudo eliminar la sede ";
                    if (Resultado)
                        Mensaje = "Se ha eliminado la sede exitosamente";

                }
                else
                {
                    Resultado = false;
                    Mensaje = "No se puede eliminar la sede porque se encuentra relacionado con el inventario";
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