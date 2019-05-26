using com.co.uan.DMiravalle.Inventario;
using System.Collections.Generic;
using System.Configuration;
using System;

namespace com.co.uan.DMiravalle.Informes
{
 public  class ServicioNotificacion:IServicioNotificaciones
    {
        INotificacionVencidos Consulta;
        Notificacion Envio;
        public ServicioNotificacion( )
        {
           Consulta = new Inventario.Inventario();
           Envio = new Notificacion();
        }
        public void NotificarElementosVencidos()
        {
            List<InventarioDTO> Vencidos = Consulta.ConsultarProductosVencidos();
            Email TipoNotificacion= new Email();
            foreach (InventarioDTO Vencido in Vencidos)
            {
                TipoNotificacion.GenerarCuerpoHTMLCorreo(Vencido,ConfigurationManager.AppSettings["PlantillaVencidos"]);
                Envio.AsignarEstrategia(TipoNotificacion);
                Envio.EjecutarNotificacion();
            }
        }

        public void NotificarProductosEscasos()
        {
            List<InventarioDTO> Vencidos = Consulta.ConsultarProductosEscasos();
            Email TipoNotificacion = new Email();
            foreach (InventarioDTO Vencido in Vencidos)
            {
                TipoNotificacion.GenerarCuerpoHTMLCorreo(Vencido, ConfigurationManager.AppSettings["PlantillaProductoEscaso"]);
                Envio.AsignarEstrategia(TipoNotificacion);
                Envio.EjecutarNotificacion();
            }
        }
    }
}
