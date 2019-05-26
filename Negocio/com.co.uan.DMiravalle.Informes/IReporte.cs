using Microsoft.Reporting.WebForms;
using System.Collections.Generic;

namespace com.co.uan.DMiravalle.Informes
{
  public   interface IReporte
    {

        LocalReport GenerarInformeVencidos();

    }
}
