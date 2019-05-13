using com.co.uan.DMiravalle.Informes;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;

namespace Web.Reporte
{
    public partial class Informes : System.Web.UI.Page
    {
        IReporte Informe;


        private IReporte ObtenerFachadaInformes() {

            if (Informe == null)
            {
                Informe = new com.co.uan.DMiravalle.Informes.Reporte();
                Session["informe"] = Informe;
            }
            else 
                Informe = ((IReporte)Session["informe"]);
            return Informe;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                GenerarInforme();
        }

        private void GenerarInforme() {
            ReporteInformes.ProcessingMode = ProcessingMode.Local;
            ReporteInformes.LocalReport.ReportPath = ObtenerFachadaInformes().GenerarInformeVencidos(" 1=1 ").ReportPath;
            ReporteInformes.LocalReport.Refresh();
        }
    }
}