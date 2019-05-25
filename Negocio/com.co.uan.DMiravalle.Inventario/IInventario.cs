using System;
using System.Collections.Generic;

namespace com.co.uan.DMiravalle.Inventario
{
   public  interface IInventario
    {

        List<InventarioDTO> ConsultarInventario(int IdLote, int IdSede, int Cantidad, int IdInventario, DateTime? FechaRegistro, int IdProducto);
        
        object  CrearInventario(int IdLote, int IdSede, int Cantidad, DateTime FechaRegistro);

        object ModificarInventario(int IdLote, int IdSede, int Cantidad, int IdInventario, DateTime FechaRegistro);

        bool EliminarInventario(int IdInventario);


        List<LoteDTO> ConsultarLote(string CodigoLote, DateTime? FechaVencimiento, int IdProducto, int IdLote, DateTime? FechaRegistro);

        List<ProductoDTO> ConsultarProducto(string Nombre, int IdTipoProducto, int IdProducto);

        List<TipoProductoDTO> ConsultarTipoProducto(string Descripcion, string CodigoReferencia, int IdTipoProducto);

        bool CrearLote(string CodigoLote, DateTime FechaVencimiento, int IdProducto, DateTime FechaRegistro);

        bool CrearProducto(string Nombre, int IdTipoProducto);

        bool EditarLote(string CodigoLote, DateTime FechaVencimiento, int IdProducto, int IdLote, DateTime FechaRegistro);

        bool EditarProducto(string Nombre, int IdTipoProducto, int IdProducto);

        bool EliminarLote(int IdLote);

        bool EliminarProducto(int IdProducto);

        bool ExisteCodigoReferencia(string CodigoReferencia, int IdTipoProducto);

        bool CrearTipoProducto(string Descripcion, string CodigoReferencia);

        bool EditarTipoProducto(string Descripcion, string CodigoReferencia, int IdTipoProducto);

        bool EliminarTipoProducto(int IdTipoProducto);

    }
}
