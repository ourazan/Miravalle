using com.co.uan.DMiravalle;
using com.co.uan.DMiravalle.Administracion;
using System;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class Controladores:Controller
    {
        protected string  Mensaje="";
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
        protected Usuario ObtenerAutenticado() {
            return new Usuario()
            {
                Nombre = "prueba1",
                Apellido = "Prueba apellido1"
            };
        }
        protected int ObtenerUsuarioAutenticado() {
            return 1;
        }
        protected void RegistarError(Exception ex) {

        }

        protected void CerrarSesion() {
            Session.Clear();
            RedirectToAction("Index", "Login");
        }
    }
}