using System.Collections.Generic;

namespace com.co.uan.DMiravalle.Informes
{
  public   interface IReporte
    {

        List<Reporte> GenerarReporteLotes(string Filtro);

    }
}
