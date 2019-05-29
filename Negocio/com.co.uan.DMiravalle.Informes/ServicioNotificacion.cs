using com.co.uan.DMiravalle.Inventario;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

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
            NotificarDetalle(Vencidos,ConfigurationManager.AppSettings["PlantillaVencidos"]);
            
          
        }

        public void NotificarProductosEscasos()
        {
            List<InventarioDTO> Vencidos = Consulta.ConsultarProductosEscasos();
            NotificarDetalle(Vencidos,ConfigurationManager.AppSettings["PlantillaProductoEscaso"]);   
        }

        private void NotificarDetalle(List<InventarioDTO> Vencidos,string Plantilla) {
            List<string> Sedes = (from sede in Vencidos
                                  group sede.SedeInventario.IdSede by sede.SedeInventario.IdSede into sed
                                  select sed.Key.ToString()).ToList();
            Email TipoNotificacion = new Email();
            foreach (string sede in Sedes)
            {
                List<InventarioDTO> VencidosSede = (from producto in Vencidos.AsEnumerable()
                                                    where producto.SedeInventario.IdSede.ToString().Equals(sede)
                                                    select producto).ToList();
                if (VencidosSede.Count > 0)
                {
                    Notificaciones.AsignarEstrategia(TipoNotificacion);
                    Notificaciones.GenerarCuerpoHTMLCorreo(VencidosSede, Plantilla);
                    Notificaciones.EjecutarNotificacion();
                }
            }

        }
    }
}
