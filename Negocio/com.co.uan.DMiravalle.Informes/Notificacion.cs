
using com.co.uan.DMiravalle.Inventario;
using System.Collections.Generic;

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

        public void GenerarCuerpoHTMLCorreo(List<InventarioDTO> ElementoVencido, string NombrePlantillaCorreo)
        {
            this.Alerta.GenerarCuerpoHTMLCorreo( ElementoVencido,  NombrePlantillaCorreo);
        }

    }
}
