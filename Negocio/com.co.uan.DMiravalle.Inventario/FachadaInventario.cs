using com.co.uan.DMiravalle.Informes;
using System;
using System.Collections.Generic;
using System.Data;

namespace com.co.uan.DMiravalle.Inventario
{
    public class FachadaInventario : IFachadaInventario, INotificacionVencidos
    {

        private IInventario Inventario;

        public FachadaInventario()
        {

            Inventario = new Inventario();
        }

        public List<Inventario> ConsultarInventario(string Filtro)
        {
            return Inventario.ConsultarInventario(Filtro);
        }

        public List<Lote> ConsultarLote(string Filtro)
        {
            return Inventario.ConsultarLote(Filtro);
        }

        public List<Producto> ConsultarProducto(string Filtro)
        {
            return Inventario.ConsultarProducto(Filtro);
        }

        public List<Inventario> ConsultarProductosVencidos(string Filtro)
        {
            return ((INotificacionVencidos)Inventario).ConsultarProductosVencidos(Filtro);
        }

        public DataTable ConsultarProductosVencidosTabla(string Filtro)
        {
            return ((INotificacionVencidos)Inventario).ConsultarProductosVencidosTabla(Filtro);
        }

        public bool CrearInventario(int IdLote, int IdSede, int Cantidad, DateTime FechaRegistro)
        {
            return Inventario.CrearInventario(IdLote, IdSede, Cantidad, FechaRegistro);
        }

        public bool CrearLote(string CodigoLote, DateTime FechaVencimiento, int IdProducto, DateTime FechaRegistro)
        {
            return Inventario.CrearLote(CodigoLote, FechaVencimiento, IdProducto, FechaRegistro);
        }

        public bool CrearProducto(string Nombre, int IdTipoProducto)
        {
            return Inventario.CrearProducto(Nombre, IdTipoProducto);
        }

        public bool EditarInventario(int IdLote, int IdSede, int Cantidad, int IdInventario, DateTime FechaRegistro)
        {
            return Inventario.ModificarInventario(IdLote, IdSede, Cantidad, IdInventario, FechaRegistro);
        }

        public bool EditarLote(string CodigoLote, DateTime FechaVencimiento, int IdProducto, int IdLote, DateTime FechaRegistro)
        {
            return Inventario.EditarLote(CodigoLote, FechaVencimiento, IdProducto, IdLote, FechaRegistro);
        }

        public bool EditarProducto(string Nombre, int IdTipoProducto, int IdProducto)
        {
            return Inventario.EditarProducto(Nombre, IdTipoProducto, IdProducto);
        }

        public bool EliminarInventario(int IdInventario)
        {
            return Inventario.EliminarInventario(IdInventario);
        }

        public bool EliminarLote(int IdLote)
        {
            return Inventario.EliminarLote(IdLote);
        }

        public bool EliminarProducto(int IdProducto)
        {
            return Inventario.EliminarProducto(IdProducto);
        }

        public List<TipoProducto> ConsultarTipoProducto(string Filtro)
        {
            return Inventario.ConsultarTipoProducto(Filtro);
        }

        public bool CrearTipoProducto(string Descripcion, string CodigoReferencia)
        {
            return Inventario.CrearTipoProducto(Descripcion, CodigoReferencia);
        }

        public bool EditarTipoProducto(string Descripcion, string CodigoReferencia, int IdTipoProducto)
        {
            return Inventario.EditarTipoProducto(Descripcion, CodigoReferencia, IdTipoProducto);
        }

        public bool EliminarTipoProducto(int IdTipoProducto)
        {
            return Inventario.EliminarTipoProducto(IdTipoProducto);
        }

        public bool ExisteCodigoReferencia(string CodigoReferencia, int IdTipoProducto)
        {
            return Inventario.ExisteCodigoReferencia(CodigoReferencia,  IdTipoProducto);
        }
        public void AsignarEjecutor(int Autenticado)
        {
            if (Inventario != null)
            {
                ((Inventario)Inventario).AsignarEjecutor(Autenticado);
                ((Inventario)Inventario).LoteProducto.AsignarEjecutor(Autenticado);
                ((Inventario)Inventario).LoteProducto.Producto.AsignarEjecutor(Autenticado);
                ((Inventario)Inventario).LoteProducto.Producto.Tipo.AsignarEjecutor(Autenticado);
            }
        }

    }
}
