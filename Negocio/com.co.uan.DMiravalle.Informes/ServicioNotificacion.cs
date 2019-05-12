using System.Collections.Generic;
using System.Configuration;

namespace com.co.uan.DMiravalle.Informes
{
 public   class ServicioNotificacion:IServicioNotificaciones
    {
        public void NotificarElementosVencidos()
        {
            INotificacionVencidos Consulta = new Inventario.Inventario();
            List<Inventario.Inventario> Vencidos = Consulta.ConsultarProductosVencidos();
            Notificacion Envio = new Notificacion();
            Email TipoNotificacion= new Email();
            foreach (Inventario.Inventario Vencido in Vencidos)
            {
                TipoNotificacion.GenerarCuerpoHTMLCorreo(Vencido,ConfigurationManager.AppSettings["PlantillaVencidos"]);
                Envio.AsignarEstrategia(TipoNotificacion);
                Envio.EjecutarNotificacion();
            }
        }
    }
}
