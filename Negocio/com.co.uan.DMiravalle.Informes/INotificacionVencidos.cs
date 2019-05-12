using com.co.uan.DMiravalle.Inventario;
using System.Collections.Generic;
namespace com.co.uan.DMiravalle.Informes
{ 

  public  interface INotificacionVencidos
    {

        List<Inventario.Inventario> ConsultarProductosVencidos();

    }
}
