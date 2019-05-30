
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controladores
    {
        public ActionResult Index()
        {
            ViewData["Autenticado"] = ObtenerAutenticado();
            if (ViewData["Autenticado"]==null)
            {
                return CerrarSesion();
            }else
            return View();
        }

     
    }
}