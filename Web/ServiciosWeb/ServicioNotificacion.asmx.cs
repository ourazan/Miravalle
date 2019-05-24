using com.co.uan.DMiravalle;
using System.Web.Services;

namespace Web.ServiciosWeb
{
    /// <summary>
    /// Summary description for ServicioNotificacion
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ServicioNotificacion : System.Web.Services.WebService
    {

        [WebMethod]
        public void NotificarProductosVencidos()
        {
            Servicio Negocio = new Servicio();
            Negocio.ObtenerServicioNotificaciones().NotificarElementosVencidos();
        }
    }
}
