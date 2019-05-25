using System;
using System.Collections.Generic;

namespace com.co.uan.DMiravalle.Inventario
{
  public   interface ILote
    {

        List<Lote> Consultar(string CodigoLote, DateTime? FechaVencimiento, int IdProducto, int IdLote, DateTime? FechaRegistro);

        bool Crear(string CodigoLote, DateTime FechaVencimiento, int IdProducto, DateTime FechaRegistro);

        bool Editar(string CodigoLote, DateTime FechaVencimiento, int IdProducto, int IdLote, DateTime FechaRegistro);

        bool Eliminar(int IdLote);

    }
}
