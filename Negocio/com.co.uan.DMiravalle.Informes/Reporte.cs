using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.Web;
using System;

namespace com.co.uan.DMiravalle.Informes
{
   public  class Reporte: IReporte
    {
        private INotificacionVencidos Inventario;

        public Reporte() {
            Inventario = new Inventario.Inventario();
        }

        public LocalReport GenerarInformeVencidos()
        {
            LocalReport Vencidos = new LocalReport();
            Vencidos.ReportPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["RutaRDL"] + "Vencidos.rdlc");
            ObtenerInformacion(ref Vencidos);
            return Vencidos;
        }

        private void ObtenerInformacion(ref LocalReport Reporte)
        {
            ReportDataSource Coleccion;
            Coleccion = new ReportDataSource("origen1", Inventario.ConsultarProductosVencidosTabla());
            Reporte.DataSources.Add(Coleccion);

        }

    }
}
