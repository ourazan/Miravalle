﻿using com.co.uan.DMiravalle.Administracion;
using com.co.uan.DMiravalle.Informes;
using System;
using System.Collections.Generic;
using Datos;
using System.Linq;
using System.Data;


namespace com.co.uan.DMiravalle.Inventario
{
 public class Inventario: IInventario, INotificacionVencidos
    {
        #region Propiedades
        private bool Resultado;
        private string Mensaje;
        private Lote LoteProducto;
        private Producto Producto;
        private TipoProducto Tipo;

        private int usuarioautenticado;
        public int UsuarioAutenticado
        {
            set { usuarioautenticado = value; }
        }
        #endregion

        #region Constructores
        public Inventario() {
            LoteProducto = new Lote();
        }

 
       
        #endregion

        #region Metodos

        private bool ExisteInventario(int IdLote ,int IdSede ,int IdInventario) {
                List<Parametros> Parametros = new List<Parametros>() {
                 new Parametros("@IdLote",IdLote,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@IdSede",IdSede,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@IdInventario",IdInventario,SqlDbType.Int,ParameterDirection.Input)
            };
            DataTable Coleccion = new Transaccion("ExisteInventario", Parametros).EjecutarDevuelveTabla();
            return Coleccion.Rows.Count > 0;
        }


        private List<InventarioDTO> MapearDatos(DataTable Coleccion) {
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





        public List<InventarioDTO> ConsultarInventario(int IdLote, int IdSede, int Cantidad, int IdInventario, DateTime? FechaRegistro)
        {
            List<Parametros> Parametros = new List<Parametros>() {
                new Parametros("@FechaRegistro",FechaRegistro==null?DBNull.Value :(object )FechaRegistro,SqlDbType.DateTime,ParameterDirection.Input)
                ,new Parametros("@Cantidad",Cantidad==-1?DBNull.Value :(object )Cantidad,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@IdLote",IdLote==0?DBNull.Value :(object )IdLote,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@IdSede",IdSede==0?DBNull.Value :(object )IdSede,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@IdInventario",IdInventario,SqlDbType.Int,ParameterDirection.Input)
            };
            DataTable Coleccion = new Transaccion("ConsultarInventario", Parametros).EjecutarDevuelveTabla();
            return MapearDatos(Coleccion);
        }

        public object CrearInventario(int IdLote, int IdSede, int Cantidad, DateTime FechaRegistro)
        {
            
            if (!ExisteInventario(IdLote, IdSede, 0))
            {
                   List<Parametros> Parametros = new List<Parametros>() {
                new Parametros("@FechaRegistro",FechaRegistro,SqlDbType.DateTime,ParameterDirection.Input)
                ,new Parametros("@Cantidad",Cantidad,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@IdLote",IdLote,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@IdSede",IdSede,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@UsuarioAutenticado",this.usuarioautenticado,SqlDbType.Int,ParameterDirection.Input)
                 ,new Parametros("@RETURN_VALUE",null,SqlDbType.Int,ParameterDirection.ReturnValue) };
                    Resultado =  Convert.ToInt32(new Transaccion("CrearInventario", Parametros).EjecutarDevuelveReturnValue()) > 0;
                    Mensaje = "No se pudo crear el inventario ";
                if (Resultado)
                    Mensaje = "Se ha creado el inventario exitosamente";
            }
            else {
                 Resultado = false;
                Mensaje = "El inventario ya se encuentra registrado para la sede seleccionada";

            }
            return new { success = true, data = Resultado, mensaje = Mensaje };
        }

        public bool EliminarInventario(int IdInventario)
        {
            List<Parametros> Parametros = new List<Parametros>() {
                 new Parametros("@IdInventario",IdInventario,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@UsuarioAutenticado",this.usuarioautenticado,SqlDbType.Int,ParameterDirection.Input)
                 ,new Parametros("@RETURN_VALUE",null,SqlDbType.Int,ParameterDirection.ReturnValue)
            };
            return Convert.ToInt32(new Transaccion("ELiminarInventario", Parametros).EjecutarDevuelveReturnValue()) > 0;
        }

        public object  ModificarInventario(int IdLote, int IdSede, int Cantidad, int IdInventario, DateTime FechaRegistro)
        {
            List<Parametros> Parametros = new List<Parametros>() {
                new Parametros("@FechaRegistro",FechaRegistro,SqlDbType.DateTime,ParameterDirection.Input)
                ,new Parametros("@Cantidad",Cantidad,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@IdLote",IdLote,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@IdSede",IdSede,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@IdInventario",IdInventario,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@UsuarioAutenticado",usuarioautenticado,SqlDbType.Int,ParameterDirection.Input)
                 ,new Parametros("@RETURN_VALUE",null,SqlDbType.Int,ParameterDirection.ReturnValue)
            };
            Resultado = Convert.ToInt32(new Transaccion("EditarInventario", Parametros).EjecutarDevuelveReturnValue()) > 0;

            Mensaje = "No se pudo editar el inventario ";
            if (Resultado)
                Mensaje = "Se ha editado el inventario exitosamente";

            return   new { success = true, data = Resultado, mensaje = Mensaje };
        }

        public List<InventarioDTO> ConsultarProductosVencidos(string Filtro)
        {
            return MapearDatos(ConsultarProductosVencidosTabla(Filtro ));
        }

        public DataTable  ConsultarProductosVencidosTabla(string Filtro)
        {
            List<Parametros> Parametros = new List<Parametros>() {
                new Parametros("@Filtro",Filtro,SqlDbType.VarChar,ParameterDirection.Input)
            };
            DataTable Coleccion = new Transaccion("ObtenerProductosaVencer", Parametros).EjecutarDevuelveTabla();
            return Coleccion;
        }

        public bool CrearLote(string CodigoLote, DateTime FechaVencimiento, int IdProducto, DateTime FechaRegistro)
        {
            return LoteProducto.Crear(CodigoLote, FechaVencimiento, IdProducto, FechaRegistro);
        }

        public bool CrearProducto(string Nombre, int IdTipoProducto)
        {
            return Producto.Crear(Nombre, IdTipoProducto);
        }

        public bool EditarLote(string CodigoLote, DateTime FechaVencimiento, int IdProducto, int IdLote, DateTime FechaRegistro)
        {
            return LoteProducto.Editar(CodigoLote, FechaVencimiento, IdProducto, IdLote, FechaRegistro);
        }

        public bool EditarProducto(string Nombre, int IdTipoProducto, int IdProducto)
        {
            return Producto.Editar(Nombre, IdTipoProducto, IdProducto);
        }

        public bool EliminarLote(int IdLote)
        {
            return LoteProducto.Eliminar(IdLote);
        }

        public bool EliminarProducto(int IdProducto)
        {
            return Producto.Eliminar(IdProducto);
        }

        public bool ExisteCodigoReferencia(string CodigoReferencia, int IdTipoProducto)
        {
          return  Tipo.ExisteCodigoReferencia(CodigoReferencia, IdTipoProducto);
        }

        public List<LoteDTO> ConsultarLote(string CodigoLote, DateTime? FechaVencimiento, int IdProducto, int IdLote, DateTime? FechaRegistro)
        {
            return LoteProducto.Consultar( CodigoLote,  FechaVencimiento,  IdProducto,  IdLote,  FechaRegistro);
        }

        public List<ProductoDTO> ConsultarProducto(string Nombre, int IdTipoProducto, int IdProducto)
        {
            return Producto.Consultar( Nombre,  IdTipoProducto,  IdProducto);
        }

        public List<TipoProductoDTO> ConsultarTipoProducto(string Descripcion, string CodigoReferencia, int IdTipoProducto)
        {
            return Tipo.Consultar( Descripcion,  CodigoReferencia,  IdTipoProducto);
        }

        public bool CrearTipoProducto(string Descripcion, string CodigoReferencia)
        {
            return Tipo.Crear(Descripcion,  CodigoReferencia);
        }

        public bool EditarTipoProducto(string Descripcion, string CodigoReferencia, int IdTipoProducto)
        {
            return Tipo.Editar(Descripcion,  CodigoReferencia,  IdTipoProducto);
        }

        public bool EliminarTipoProducto(int IdTipoProducto)
        {
            return Tipo.Eliminar(IdTipoProducto);
        }
        #endregion
    }
}
