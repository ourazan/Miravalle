using com.co.uan.DMiravalle;
using com.co.uan.DMiravalle.Inventario;
using System.Collections.Generic;

using System.Web.Services;

namespace Web.ServiciosWeb
{
    /// <summary>
    /// Summary description for ServiciosMiravalle
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ServiciosMiravalle : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public void NotificarProductos()
        {
            Servicio Negocio = new Servicio();
            Negocio.ObtenerServicioNotificaciones().NotificarElementosVencidos();
            Negocio.ObtenerServicioNotificaciones().NotificarProductosEscasos();
        }

        [WebMethod]
        public List<ProductoDTO> ConsultarProducto(string Nombre, int IdTipoProducto, int IdProducto)
        {
            Servicio Negocio = new Servicio();
            return Negocio.ObtenerFachadaInventario().ConsultarProducto(Nombre, IdTipoProducto, IdProducto);
        }
    }
}
