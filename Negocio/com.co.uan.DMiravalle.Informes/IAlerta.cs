
using com.co.uan.DMiravalle.Inventario;

namespace com.co.uan.DMiravalle.Informes
{
   public  interface IAlerta
    {

        void EnviarNotificacion();
        void GenerarCuerpoHTMLCorreo(InventarioDTO ElementoVencido, string NombrePlantillaCorreo);
    }
}
