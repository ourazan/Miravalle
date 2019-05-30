
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ReportesController : Controladores
    {
        // GET: Reportes
        public ActionResult Index()
        {
            ViewData["Autenticado"] = ObtenerAutenticado();
            return RetornarVista();
        }
    }
}