

using System;

namespace com.co.uan.DMiravalle.Inventario
{
   public class LoteDTO
    {

        private string codigolote;

        private ProductoDTO producto;

        private int idlote;

        private DateTime fechavencimiento;

        private DateTime fecharegistro;

        public LoteDTO(string codigolote, ProductoDTO producto, int idlote, DateTime fechavencimiento, DateTime fecharegistro)
        {
            this.codigolote = codigolote;
            this.producto = producto;
            this.idlote = idlote;
            this.fechavencimiento = fechavencimiento;
            this.fecharegistro = fecharegistro;
        }

        public string CodigoLote {  get{ return codigolote; } }

        public ProductoDTO Producto {  get{ return producto; } }

        public int IdLote {  get{ return idlote; } }

        public DateTime FechaVencimiento {  get{ return fechavencimiento; } }

        public DateTime FechaRegistro {  get{ return fecharegistro; } }
    }
}
