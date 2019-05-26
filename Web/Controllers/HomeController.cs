
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controladores
    {
        public ActionResult Index()
        {
            ViewData["Autenticado"] = ObtenerAutenticado();
            return View();
        }

     
    }
}