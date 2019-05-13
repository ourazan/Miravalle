using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.Web;
namespace com.co.uan.DMiravalle.Informes
{
   public  class Reporte: IReporte
    {
        private INotificacionVencidos Inventario;

        public Reporte() {
            Inventario = new Inventario.Inventario();
        }

        public LocalReport GenerarInformeVencidos(string Filtro)
        {
            LocalReport Vencidos = new LocalReport();
            Vencidos.ReportPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["RutaRDL"] + "Vencidos.rdlc");
            ObtenerInformacion(ref Vencidos, Filtro);
            return Vencidos;
        }

        private void ObtenerInformacion(ref LocalReport Reporte, string Filtro)
        {
            ReportDataSource Coleccion;
            Coleccion = new ReportDataSource("origen1", Inventario.ConsultarProductosVencidos(Filtro));
            Reporte.DataSources.Add(Coleccion);

        }

    }
}
