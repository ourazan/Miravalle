using com.co.uan.DMiravalle.Administracion;
using System;

namespace com.co.uan.DMiravalle.Inventario
{
   public class InventarioDTO
    {
        private SedeDTO sedeinventario;

        private LoteDTO loteproducto;

        private int idinventario;

        private int cantidad;

        private DateTime fecharegistro;

        public SedeDTO SedeInventario { get { return sedeinventario; } }

        public LoteDTO LoteProducto { get { return loteproducto; } }

        public int IdInventario { get { return idinventario; } }

        public int Cantidad { get { return cantidad; } }

        public DateTime FechaRegistro { get { return fecharegistro; } }

        public InventarioDTO(SedeDTO sedeinventario, LoteDTO loteproducto, int idinventario, int cantidad, DateTime fecharegistro)
        {
            this.sedeinventario = sedeinventario;
            this.loteproducto = loteproducto;
            this.idinventario = idinventario;
            this.cantidad = cantidad;
            this.fecharegistro = fecharegistro;
        }
    }
}
