using System.Collections.Generic;

namespace com.co.uan.DMiravalle.Inventario
{
   public  interface ITipoProducto
    {
        List<TipoProductoDTO> Consultar(string Descripcion, string CodigoReferencia, int IdTipoProducto);
        bool Crear(string Descripcion, string CodigoReferencia);
        bool Editar(string Descripcion, string CodigoReferencia, int IdTipoProducto);
        bool Eliminar(int IdTipoProducto);
        bool ExisteCodigoReferencia(string CodigoReferencia, int IdTipoProducto);
    }
}
