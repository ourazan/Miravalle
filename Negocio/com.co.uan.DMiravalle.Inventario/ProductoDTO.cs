

namespace com.co.uan.DMiravalle.Inventario
{
   public class ProductoDTO
    {
        public string nombreproducto;

        public int idproducto;

        public TipoProductoDTO tipo;

        public ProductoDTO() { }

        public ProductoDTO(string nombreproducto, int idproducto, TipoProductoDTO tipo)
        {
            this.nombreproducto = nombreproducto;
            this.idproducto = idproducto;
            this.tipo = tipo;
        }

        public string NombreProducto { get { return nombreproducto; } }

        public int IdProducto { get { return idproducto; } }

        public TipoProductoDTO Tipo { get { return tipo; } }
    }
}
