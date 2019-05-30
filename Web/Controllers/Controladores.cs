using com.co.uan.DMiravalle;
using com.co.uan.DMiravalle.Administracion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class Controladores:Controller
    {
        protected bool Resultado = false;
        Servicio Negocio=null ;
        protected Servicio ObtenerNegocio() {
            if (Session["Negocio"] == null)
            {
                Negocio = new Servicio();
                Session["Negocio"]=Negocio;
            }
            else {
                Negocio = (Servicio)Session["Negocio"];

            }
            Negocio.AsignarEjecutor(ObtenerUsuarioAutenticado());
            return  Negocio;
        }
        protected UsuarioDTO ObtenerAutenticado()
        {
            if (Session["Autenticado"] != null)
            {
                return (UsuarioDTO)Session["Autenticado"];
            }
            else
            {
                CerrarSesion();
                return null;
            }
        }
        protected int ObtenerUsuarioAutenticado()
        {
            if (ObtenerAutenticado() != null)
            {
                return ObtenerAutenticado().IdUsuario;
            }
            else
            {
                return 0;
            }
        }
        protected void RegistarError(Exception ex) {
            String RutaLog = Server.MapPath(ConfigurationManager.AppSettings["RutaLog"] + DateTime.Now.Year.ToString() + string.Format(DateTime.Now.Month.ToString(), "00") + string.Format(DateTime.Now.Day.ToString(), "00") + ".txt");
            StreamWriter Guardar = new StreamWriter(RutaLog, true);
            StringBuilder Detalle = new StringBuilder();
            Detalle.AppendLine("Error:"+ex.Message);
            Detalle.AppendLine("Detalle:"+ex.StackTrace);
            Detalle.AppendLine("Usuario:" + ObtenerUsuarioAutenticado().ToString());
            Guardar.WriteLine(Detalle.ToString());
            Guardar.Close();
        }

        [HttpGet]
        public ActionResult CerrarSesion()  {
            Session.Clear();
            return  RedirectToAction("Index", "Login");
        }

        protected List<SelectListItem> AdicionarValorDefecto(List<SelectListItem> Items) {
            Items.Insert(0, new SelectListItem() { Text ="SELECCIONE",Value="" });
            return Items;

        }
    }
}