using com.co.uan.DMiravalle.Administracion;
using com.co.uan.DMiravalle.Informes;
using com.co.uan.DMiravalle.Inventario;
namespace com.co.uan.DMiravalle
{
    public   class ConstructorFachada
    {
        #region Propiedades
        private  IServicioNotificaciones ObjetoNotificaciones;
        private  IFachadaAdministracion ObjetoAdministracion;
        private  IFachadaInventario ObjetoFachadaInventario;
        #endregion
        #region Constructor
        public ConstructorFachada() {
            ObjetoAdministracion=new FachadaAdministracion();
            ObjetoNotificaciones = new ServicioNotificacion();
            ObjetoFachadaInventario=new FachadaInventario();
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
        public   IFachadaInventario ObtenerFachadaInventario() {
            return ObjetoFachadaInventario; 
        }

        public void AsignarEjecutor(int Autenticado) {
            ObjetoAdministracion.AsignarEjecutor(Autenticado);
            ObjetoFachadaInventario.AsignarEjecutor(Autenticado);
        }
        #endregion

    }
}
