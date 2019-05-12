using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class SedeController : Controladores
    {
        // GET: Sede
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult crearRegisro()
        {
            string Mensaje = "";
            int Resultado = 0;

            try
            {
                if (Request["id"] == "0")
                {
                    //Resultado = FachadaAdministracion.CrearSede(Request["txtCiudad"], Request["txtDireccion"]);
                    Mensaje = "No se pudo crear el usuario ";

                    if (Resultado > 0)
                    {
                        Mensaje = "Se ha creado el usuario exitosamente";
                    }

                }
                else
                {
                    //Resultado = FachadaAdministracion.EditarSede(Request["txtCiudad"], Request["txtDireccion"]);
                    Mensaje = "No se pudo editar el Usuario ";
                    if (Resultado > 0)
                        Mensaje = "Se ha editado el usuario exitosamente";
                }


            }
            catch (Exception ex)
            {
                Mensaje = "Se presento inconveniente al realizar la accion";
            }

            return Json(new { success = true, data = Resultado, mensaje = Mensaje });

        }

    }
}