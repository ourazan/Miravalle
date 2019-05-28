using com.co.uan.DMiravalle.Administracion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace com.co.uan.DMiravalle.Inventario
{
   public  class InventarioRowMaper
    {
        public List<InventarioDTO> MapearDatos(DataTable Coleccion)
        {
            List<InventarioDTO> Resultado = (from fila in Coleccion.AsEnumerable()
                                             select new InventarioDTO(
                                                                 new SedeDTO(fila["NombreSede"].ToString()
                                                                     , fila["Direccion"].ToString()
                                                                     , fila["Ciudad"].ToString()
                                                                     , Int32.Parse(fila["IdSede"].ToString())
                                                                     , new UsuarioDTO(fila["Nombre"].ToString()
                                                                                  , fila["Apellido"].ToString()
                                                                                  , fila["Correo"].ToString()
                                                                                  , new SedeDTO()
                                                                                  , Int32.Parse(fila["IdAdministrador"].ToString())
                                                                                  , 0
                                                                                  , fila["NombreUsuario"].ToString()
                                                                                  , fila["Clave"].ToString()
                                                                                  , Convert.ToInt32(fila["Perfil"])
                                                                                  ))
                                                                       , new LoteDTO(fila["CodigoLote"].ToString()
                                                                                 , new ProductoDTO(
                                                                                     fila["NombreProducto"].ToString()
                                                                                     , Convert.ToInt32(fila["IdProducto"])
                                                                                     , new TipoProductoDTO(Convert.ToInt32(fila["IdTipoProducto"])
                                                                                                     , fila["CodigoReferencia"].ToString()
                                                                                                     , fila["Descripcion"].ToString())
                                                                                     )
                                                                                 , Convert.ToInt32(fila["IdLote"])
                                                                                 , Convert.ToDateTime(fila["FechaVencimiento"])
                                                                                 , Convert.ToDateTime(fila["Lote_FechaRegistro"]))
                                                                         , Convert.ToInt32(fila["IdInventario"])
                                                                         , Convert.ToInt32(fila["Cantidad"])
                                                                         , Convert.ToDateTime(fila["FechaRegistro"]))


                                                    ).ToList();

            return Resultado;
        }

    }
}
