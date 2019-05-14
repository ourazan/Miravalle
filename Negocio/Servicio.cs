using com.co.uan.DMiravalle.Administracion;
using com.co.uan.DMiravalle.Informes;
using com.co.uan.DMiravalle.Inventario;
namespace com.co.uan.DMiravalle
{
    public  class Servicio
    {
        #region Propiedades
        private  IServicioNotificaciones ObjetoNotificaciones;
        private  IFachadaAdministracion ObjetoAdministracion;
        private  IInventario ObjetoFachadaInventario;
        #endregion
        #region Constructor
        public Servicio() {
            ObjetoAdministracion=new FachadaAdministracion();
            ObjetoNotificaciones = new ServicioNotificacion();
            ObjetoFachadaInventario=new Inventario.Inventario();
        }   
        #endregion
        #region Metodos
       public   IServicioNotificaciones ObtenerServicioNotificaciones()
        {
            return ObjetoNotificaciones;
        }
        public  IFachadaAdministracion ObtenerFachadaAdministrativa()
        {
            return ObjetoAdministracion;
        }
        public IInventario ObtenerFachadaInventario() {
            return ObjetoFachadaInventario; 
        }

        public void AsignarEjecutor(int Autenticado) {
            ObjetoAdministracion.AsignarEjecutor(Autenticado);
            ((Inventario.Inventario)ObjetoFachadaInventario).AsignarEjecutor(Autenticado);
            ((Inventario.Inventario)ObjetoFachadaInventario).LoteProducto.AsignarEjecutor(Autenticado);
            ((Inventario.Inventario)ObjetoFachadaInventario).LoteProducto.Producto.AsignarEjecutor(Autenticado);
            ((Inventario.Inventario)ObjetoFachadaInventario).LoteProducto.Producto.Tipo.AsignarEjecutor(Autenticado);
        }
        #endregion

    }
}
