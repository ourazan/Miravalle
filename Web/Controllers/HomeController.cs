
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controladores
    {
        public ActionResult Index()
        {
            ViewData["Autenticado"] = ObtenerAutenticado();
            ObtenerNegocio().ObtenerServicioNotificaciones().NotificarProductosEscasos();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}