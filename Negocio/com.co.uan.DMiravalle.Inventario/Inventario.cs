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

        public  Sede SedeInventario { get; set; }

        public Lote LoteProducto { get; set; }

        public int IdInventario { get; set; }

        public int Cantidad { get; set; }

        public DateTime FechaRegistro { get; set; }

        public int UsuarioAutenticado { get; set; }
        #endregion

        #region Constructores
        public Inventario() {
            SedeInventario = new Sede();
            LoteProducto = new Lote();
        }

        public Inventario( int idInventario)
        {
           
            IdInventario = idInventario;
            List<Inventario> Registro = ConsultarInventario(" IdInventario="+ idInventario);
            if (Registro.Count >0) {
                SedeInventario =Registro[0]. SedeInventario;
                LoteProducto = Registro[0].LoteProducto;
                Cantidad = Registro[0].Cantidad;
                FechaRegistro = Registro[0].FechaRegistro;
            }
        }

        public Inventario( Sede sedeInventario, Lote loteProducto, int idInventario, int cantidad, DateTime fechaRegistro)
        {
           
            IdInventario = idInventario;
            SedeInventario = sedeInventario;
            LoteProducto = loteProducto;
            Cantidad = cantidad;
            FechaRegistro = fechaRegistro;
        }
        #endregion

        #region Metodos

        private bool ExisteInventario(int Idlote ,int IdSede ,int IdInventario) {

            return ConsultarInventario(" IdLote="+ Idlote.ToString() + " and IdSede="+ IdSede.ToString()+" and  IdInventario not in(" + IdInventario .ToString ()+")").Count>0;
        }


        private List<Inventario> MapearDatos(DataTable Coleccion) {
            List<Inventario> Resultado = (from fila in Coleccion.AsEnumerable()
                                          select new Inventario(
                                                              new Sede(fila["NombreSede"].ToString()
                                                                  , fila["Direccion"].ToString()
                                                                  , fila["Ciudad"].ToString()
                                                                  , Int32.Parse(fila["IdSede"].ToString())
                                                                  , new Usuario(fila["Nombre"].ToString()
                                                                               , fila["Apellido"].ToString()
                                                                               , fila["Correo"].ToString()
                                                                               , new Sede()
                                                                               , Int32.Parse(fila["IdAdministrador"].ToString())
                                                                               , 0
                                                                               , fila["NombreUsuario"].ToString()
                                                                               , fila["Clave"].ToString()
                                                                               , Convert.ToInt32(fila["Perfil"])
                                                                               ))
                                                                    , new Lote(fila["CodigoLote"].ToString()
                                                                              , new Producto(
                                                                                  fila["NombreProducto"].ToString()
                                                                                  , Convert.ToInt32(fila["IdProducto"])
                                                                                  , new TipoProducto(Convert.ToInt32(fila["IdTipoProducto"])
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





        public List<Inventario> ConsultarInventario(string Filtro)
        {
            List<Parametros> Parametros = new List<Parametros>() {
                new Parametros("@filtro",Filtro,SqlDbType.VarChar,ParameterDirection.Input)
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
                ,new Parametros("@UsuarioAutenticado",this.UsuarioAutenticado,SqlDbType.Int,ParameterDirection.Input)
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
                ,new Parametros("@UsuarioAutenticado",this.UsuarioAutenticado,SqlDbType.Int,ParameterDirection.Input)
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
                ,new Parametros("@UsuarioAutenticado",UsuarioAutenticado,SqlDbType.Int,ParameterDirection.Input)
                 ,new Parametros("@RETURN_VALUE",null,SqlDbType.Int,ParameterDirection.ReturnValue)
            };
            Resultado = Convert.ToInt32(new Transaccion("EditarInventario", Parametros).EjecutarDevuelveReturnValue()) > 0;

            Mensaje = "No se pudo editar el inventario ";
            if (Resultado)
                Mensaje = "Se ha editado el inventario exitosamente";

            return   new { success = true, data = Resultado, mensaje = Mensaje };
        }

        public List<Inventario> ConsultarProductosVencidos(string Filtro)
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
            return LoteProducto.Producto.Crear(Nombre, IdTipoProducto);
        }

        public bool EditarLote(string CodigoLote, DateTime FechaVencimiento, int IdProducto, int IdLote, DateTime FechaRegistro)
        {
            return LoteProducto.Editar(CodigoLote, FechaVencimiento, IdProducto, IdLote, FechaRegistro);
        }

        public bool EditarProducto(string Nombre, int IdTipoProducto, int IdProducto)
        {
            return LoteProducto.Producto.Editar(Nombre, IdTipoProducto, IdProducto);
        }

        public bool EliminarLote(int IdLote)
        {
            return LoteProducto.Eliminar(IdLote);
        }

        public bool EliminarProducto(int IdProducto)
        {
            return LoteProducto.Producto.Eliminar(IdProducto);
        }

        public bool ExisteCodigoReferencia(string CodigoReferencia, int IdTipoProducto)
        {
          return  LoteProducto.Producto.Tipo.ExisteCodigoReferencia(CodigoReferencia, IdTipoProducto);
        }

        public List<Lote> ConsultarLote(string Filtro)
        {
            return LoteProducto.Consultar(Filtro);
        }

        public List<Producto> ConsultarProducto(string Filtro)
        {
            return LoteProducto.Producto.Consultar(Filtro);
        }

        public List<TipoProducto> ConsultarTipoProducto(string Filtro)
        {
            return LoteProducto.Producto.Tipo.Consultar(Filtro);
        }

        public bool CrearTipoProducto(string Descripcion, string CodigoReferencia)
        {
            return LoteProducto.Producto.Tipo.Crear(Descripcion,  CodigoReferencia);
        }

        public bool EditarTipoProducto(string Descripcion, string CodigoReferencia, int IdTipoProducto)
        {
            return LoteProducto.Producto.Tipo.Editar(Descripcion,  CodigoReferencia,  IdTipoProducto);
        }

        public bool EliminarTipoProducto(int IdTipoProducto)
        {
            return LoteProducto.Producto.Tipo.Eliminar(IdTipoProducto);
        }
        #endregion
    }
}
