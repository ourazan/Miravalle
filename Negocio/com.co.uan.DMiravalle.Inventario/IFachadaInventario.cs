﻿using System;
using System.Collections.Generic;

namespace com.co.uan.DMiravalle.Inventario
{
   public  interface IFachadaInventario
    {

        List<Inventario> ConsultarInventario(string Filtro);

        List<Lote> ConsultarLote(string Filtro);

        List<Producto> ConsultarProducto(string Filtro);

        List<TipoProducto> ConsultarTipoProducto(string Filtro);

        bool CrearTipoProducto(string Descripcion, string CodigoReferencia);

        bool EditarTipoProducto(string Descripcion, string CodigoReferencia,int IdTipoProducto);

        bool EliminarTipoProducto(int IdTipoProducto);

        bool CrearInventario(int IdLote, int IdSede, int Cantidad, DateTime FechaRegistro);

        bool EditarInventario(int IdLote, int IdSede, int Cantidad, int IdInventario, DateTime FechaRegistro);

        bool EliminarInventario(int IdInventario);

        bool CrearLote(string CodigoLote, DateTime FechaVencimiento, int IdProducto, DateTime FechaRegistro);

        bool CrearProducto(string Nombre, int IdTipoProducto);

        bool EditarLote(string CodigoLote, DateTime FechaVencimiento, int IdProducto, int IdLote, DateTime FechaRegistro);

        bool EditarProducto(string Nombre, int IdTipoProducto, int IdProducto);

        bool EliminarLote(int IdLote);

        bool EliminarProducto(int IdProducto);

        bool ExisteCodigoReferencia(string CodigoReferencia, int IdTipoProducto);

        void AsignarEjecutor(int Autenticado);
    }
}
