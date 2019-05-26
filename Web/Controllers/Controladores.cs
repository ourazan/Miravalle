using com.co.uan.DMiravalle;
using com.co.uan.DMiravalle.Administracion;
using System;
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
        protected UsuarioDTO ObtenerAutenticado() {
            return new UsuarioDTO("prueba1", "Prueba apellido1",string.Empty,null,0,0,string .Empty,string.Empty,0);
        }
        protected int ObtenerUsuarioAutenticado() {
            return 1;
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

        protected void CerrarSesion() {
            Session.Clear();
            RedirectToAction("Index", "Login");
        }
    }
}