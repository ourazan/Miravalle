
using com.co.uan.DMiravalle.Inventario;

namespace com.co.uan.DMiravalle.Informes
{ 
 public   class Notificacion
    {
        private IAlerta Alerta;

        public void AsignarEstrategia(IAlerta Alerta)
        {
            this.Alerta = Alerta;
        }

        public void EjecutarNotificacion()
        {
            this.Alerta.EnviarNotificacion();
        }

        public void GenerarCuerpoHTMLCorreo(InventarioDTO ElementoVencido, string NombrePlantillaCorreo)
        {
            this.Alerta.GenerarCuerpoHTMLCorreo( ElementoVencido,  NombrePlantillaCorreo);
        }

    }
}
