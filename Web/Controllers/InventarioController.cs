﻿using System;
using System.Linq;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class InventarioController : Controladores
    {
        // GET: Inventario
        public ActionResult Index()
        {
            ViewData["Inventario"] = ObtenerNegocio().ObtenerFachadaInventario().ConsultarInventario (Convert.ToInt32( Request["IdLote"]),0,-1,0,null,0);
            return PartialView();
        }
        [HttpGet]
        public ActionResult Edicion()
        {
            SelectList Sedes;
            var Items = (from sede in ObtenerNegocio().ObtenerFachadaAdministrativa().ConsultarSede(string.Empty, string.Empty, string.Empty, 0, 0)
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
                Resultado = false;
            }
            return Json(new { success = true, data = Resultado });
        }

        [HttpPost]
        public ActionResult Eliminar()
        {
            try
            {
                    Resultado = ObtenerNegocio().ObtenerFachadaInventario().EliminarInventario(Convert.ToInt32(Request["IdInventario"]));
            }
            catch (Exception ex)
            {
                RegistarError(ex);
                Resultado = false;
            }
            return Json(new { success = true, data = Resultado });
        }

        [HttpPost]
        public ActionResult ExisteLoteXSede()
        {
            try
            {
                Resultado = ObtenerNegocio().ObtenerFachadaInventario().ConsultarInventario(
                      Convert.ToInt32(Request["hddIDLote"])
                    , Convert.ToInt32(Request["SedeInventario"])
                    , -1
                    ,0
                    ,null
                    ,0).Count>0;
            }
            catch (Exception ex)
            {
                RegistarError(ex);
            }
            return Json(new { success = true, data = Resultado});
        }
    }
}