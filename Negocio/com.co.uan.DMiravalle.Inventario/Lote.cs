﻿using System;
using System.Collections.Generic;
using System.Data;
using Datos;
using System.Linq;


namespace com.co.uan.DMiravalle.Inventario
{
   public  class Lote: ILote
    {
        #region Propiedades
       
        public  string CodigoLote { get; set; }

        public Producto Producto { get; set; }

        public int IdLote { get; set; }

        public DateTime FechaVencimiento { get; set; }

        public DateTime FechaRegistro { get; set; }

        private int usuarioautenticado;
        public int UsuarioAutenticado
        {
            set { usuarioautenticado = value; }
        }

        #endregion
        #region constructores
        public Lote()
        {
            Producto = new Producto();
        }
        public Lote(int idLote) {
            IdLote = idLote;
            List<Lote> Registro = Consultar( string .Empty ,null ,0,idLote,null );
            if (Registro.Count>0)
            {

                CodigoLote = Registro[0].CodigoLote;
                Producto = Registro[0].Producto;
                FechaVencimiento = Registro[0].FechaVencimiento;
                FechaRegistro = Registro[0].FechaRegistro;
            }
        }

        public Lote( string codigoLote, Producto producto, int idLote, DateTime fechaVencimiento, DateTime fechaRegistro)
        {
            IdLote = idLote;
            CodigoLote = codigoLote;
            Producto = producto;
            FechaVencimiento = fechaVencimiento;
            FechaRegistro = fechaRegistro;
        }

        #endregion
        #region Funciones
        public List<Lote> Consultar(string CodigoLote, DateTime? FechaVencimiento, int IdProducto, int IdLote, DateTime? FechaRegistro)
        {
            List<Parametros> Parametros = new List<Parametros>() {
                 new Parametros("@CodigoLote",string.IsNullOrEmpty ( CodigoLote)?DBNull .Value :(object)CodigoLote,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@FechaRegistro",FechaRegistro==null?DBNull.Value: (object)FechaRegistro,SqlDbType.DateTime,ParameterDirection.Input)
                ,new Parametros("@FechaVencimiento",FechaVencimiento==null?DBNull.Value: (object)FechaVencimiento,SqlDbType.DateTime,ParameterDirection.Input)
                ,new Parametros("@IdProducto",IdProducto==0?DBNull.Value:(object)IdProducto,SqlDbType.Int,ParameterDirection.Input)
                 ,new Parametros("@IdLote",IdLote==0?DBNull.Value:(object)IdLote,SqlDbType.Int,ParameterDirection.Input)
            };
            DataTable Coleccion = new Transaccion("ConsultarLote", Parametros).EjecutarDevuelveTabla();
            List<Lote> Resultado = (from fila in Coleccion.AsEnumerable()
                                        select new Lote( fila["CodigoLote"].ToString()
                                                                ,new Producto(
                                                                    fila["NombreProducto"].ToString()
                                                                    , Convert.ToInt32(fila["IdProducto"])
                                                                    , new TipoProducto(Convert.ToInt32(fila["IdTipoProducto"])
                                                                                    , fila["CodigoReferencia"].ToString()
                                                                                    , fila["Descripcion"].ToString())
                                                                    )
                                                                , Convert.ToInt32(fila["IdLote"])
                                                                , Convert.ToDateTime(fila["FechaVencimiento"])
                                                                , Convert.ToDateTime(fila["FechaRegistro"])
                                                                

                                                        )).ToList();

            return Resultado;
        }

        public bool Crear(string CodigoLote, DateTime FechaVencimiento, int IdProducto, DateTime FechaRegistro)
        {
            List<Parametros> Parametros = new List<Parametros>() {
                new Parametros("@CodigoLote",CodigoLote,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@FechaRegistro",FechaRegistro,SqlDbType.DateTime,ParameterDirection.Input)
                ,new Parametros("@FechaVencimiento",FechaVencimiento,SqlDbType.DateTime,ParameterDirection.Input)
                ,new Parametros("@IdProducto",IdProducto,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@UsuarioAutenticado",this.UsuarioAutenticado,SqlDbType.Int,ParameterDirection.Input)
                 ,new Parametros("@RETURN_VALUE",null,SqlDbType.Int,ParameterDirection.ReturnValue)
            };
            return Convert.ToInt32(new Transaccion("CrearLote", Parametros).EjecutarDevuelveReturnValue()) > 0;
        }

        public bool Editar(string CodigoLote, DateTime FechaVencimiento, int IdProducto, int IdLote, DateTime FechaRegistro)
        {
            List<Parametros> Parametros = new List<Parametros>() {
                new Parametros("@CodigoLote",CodigoLote,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@FechaRegistro",FechaRegistro,SqlDbType.DateTime,ParameterDirection.Input)
                ,new Parametros("@FechaVencimiento",FechaVencimiento,SqlDbType.DateTime,ParameterDirection.Input)
                ,new Parametros("@IdProducto",IdProducto,SqlDbType.Int,ParameterDirection.Input)
                 ,new Parametros("@IdLote",IdLote,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@UsuarioAutenticado",this.UsuarioAutenticado,SqlDbType.Int,ParameterDirection.Input)
                 ,new Parametros("@RETURN_VALUE",null,SqlDbType.Int,ParameterDirection.ReturnValue)
            };
            return Convert.ToInt32(new Transaccion("EditarLote", Parametros).EjecutarDevuelveReturnValue()) > 0;
        }

        public bool Eliminar(int IdLote)
        {
            List<Parametros> Parametros = new List<Parametros>() {
                new Parametros("@IdLote",IdLote,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@UsuarioAutenticado",this.UsuarioAutenticado,SqlDbType.Int,ParameterDirection.Input)
                 ,new Parametros("@RETURN_VALUE",null,SqlDbType.Int,ParameterDirection.ReturnValue)
            };
            return Convert.ToInt32(new Transaccion("EliminarLote", Parametros).EjecutarDevuelveReturnValue()) > 0;
        }

        #endregion
     
    }
}
