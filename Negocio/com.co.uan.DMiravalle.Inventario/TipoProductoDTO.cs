

namespace com.co.uan.DMiravalle.Inventario
{
  public   class TipoProductoDTO
    {

        #region Propiedades

        private int idtipoproducto;
        private string codigoReferencia;
        private string descripcion;


        public int IdTipoProducto { get { return idtipoproducto; } }
        public string CodigoReferencia { get { return codigoReferencia; } }
        public string Descripcion { get { return descripcion; } }

        #endregion
        #region Constructores    
        public TipoProductoDTO() { }
        public TipoProductoDTO(int idTipoProducto, string codigoReferencia, string descripcion)
        {
            this.idtipoproducto = idTipoProducto;
            this.codigoReferencia = codigoReferencia;
            this.descripcion = descripcion;
        }

        #endregion
    }
}
