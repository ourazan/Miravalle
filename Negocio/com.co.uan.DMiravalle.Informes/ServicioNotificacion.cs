﻿using System.Collections.Generic;
using System.Configuration;

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
            List<Inventario.Inventario> Vencidos = Consulta.ConsultarProductosVencidos(" 1=1 ");
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
