using com.co.uan.DMiravalle.Inventario;
using System.Collections.Generic;
using System.Configuration;
using System;

namespace com.co.uan.DMiravalle.Informes
{
 public  class ServicioNotificacion:IServicioNotificaciones
    {
        INotificacionVencidos Consulta;
        Notificacion Notificaciones;
        public ServicioNotificacion( )
        {
           Consulta = new Inventario.Inventario();
           Notificaciones = new Notificacion();
        }
        public void NotificarElementosVencidos()
        {
            List<InventarioDTO> Vencidos = Consulta.ConsultarProductosVencidos();
            Email TipoNotificacion= new Email();
            foreach (InventarioDTO Vencido in Vencidos)
            {
                Notificaciones.GenerarCuerpoHTMLCorreo(Vencido,ConfigurationManager.AppSettings["PlantillaVencidos"]);
                Notificaciones.AsignarEstrategia(TipoNotificacion);
                Notificaciones.EjecutarNotificacion();
            }
        }

        public void NotificarProductosEscasos()
        {
            List<InventarioDTO> Vencidos = Consulta.ConsultarProductosEscasos();
            Email TipoNotificacion = new Email();
            foreach (InventarioDTO Vencido in Vencidos)
            {
                Notificaciones.GenerarCuerpoHTMLCorreo(Vencido, ConfigurationManager.AppSettings["PlantillaProductoEscaso"]);
                Notificaciones.AsignarEstrategia(TipoNotificacion);
                Notificaciones.EjecutarNotificacion();
            }
        }
    }
}
