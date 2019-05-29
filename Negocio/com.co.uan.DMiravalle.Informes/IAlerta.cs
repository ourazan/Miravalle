
using com.co.uan.DMiravalle.Inventario;
using System.Collections.Generic;

namespace com.co.uan.DMiravalle.Informes
{
   public  interface IAlerta
    {

        void EnviarNotificacion();
        void GenerarCuerpoHTMLCorreo(List<InventarioDTO>  ElementoVencido, string NombrePlantillaCorreo);
    }
}
