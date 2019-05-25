using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Datos;

namespace com.co.uan.DMiravalle.Inventario
{
    public class TipoProducto :  ITipoProducto
    {
        #region Propiedades
      
        private int usuarioautenticado;
        public int UsuarioAutenticado
        {
            set { this.usuarioautenticado = value; }
        }

        #endregion
       
        #region Metodos
        public List<TipoProductoDTO> Consultar(string Descripcion, string CodigoReferencia, int IdTipoProducto)
        {
            List<Parametros> Parametros = new List<Parametros>() {
                new Parametros("@Descripcion",string.IsNullOrEmpty( Descripcion)?DBNull.Value :(object )Descripcion,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@CodigoReferencia",string.IsNullOrEmpty( CodigoReferencia) ? DBNull.Value : (object)CodigoReferencia, SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@IdTipoProducto",IdTipoProducto==0?DBNull.Value:(object )IdTipoProducto, SqlDbType.Int,ParameterDirection.Input)
            };
            DataTable Coleccion = new Transaccion("ConsultarTipoProducto", Parametros).EjecutarDevuelveTabla();
            List<TipoProductoDTO> Resultado = (from fila in Coleccion.AsEnumerable()
                                    select new TipoProductoDTO( Int32.Parse(fila["IdTipoProducto"].ToString())  
                                                            , fila["CodigoReferencia"].ToString()
                                                            , fila["Descripcion"].ToString()  )
                                                            ).ToList();


            return Resultado;
        }

        public bool Crear(string Descripcion, string CodigoReferencia)
        {
                List<Parametros> Parametros = new List<Parametros>() {
                new Parametros("@Descripcion",Descripcion,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@CodigoReferencia",CodigoReferencia,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@UsuarioAutenticado",usuarioautenticado,SqlDbType.Int,ParameterDirection.Input)
                 ,new Parametros("@RETURN_VALUE",null,SqlDbType.Int,ParameterDirection.ReturnValue)
                
            };
            return Convert.ToInt32(new Transaccion("CrearTipoProducto", Parametros).EjecutarDevuelveReturnValue()) > 0;
        }

        public bool Editar(string Descripcion, string CodigoReferencia, int IdTipoProducto)
        {
                List<Parametros> Parametros = new List<Parametros>() {
                new Parametros("@Descripcion",Descripcion,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@CodigoReferencia",CodigoReferencia,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@IdTipoProducto",IdTipoProducto,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@UsuarioAutenticado",usuarioautenticado,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@RETURN_VALUE",null,SqlDbType.Int,ParameterDirection.ReturnValue)
            };
            return Convert.ToInt32(new Transaccion("EditarTipoProducto", Parametros).EjecutarDevuelveReturnValue()) > 0;
        }

        public bool Eliminar(int IdTipoProducto)
        {
                List<Parametros> Parametros = new List<Parametros>() {
                new Parametros("@IdTipoProducto",IdTipoProducto,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@UsuarioAutenticado",usuarioautenticado,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@RETURN_VALUE",null,SqlDbType.Int,ParameterDirection.ReturnValue)
            };
            return Convert.ToInt32(new Transaccion("EliminarTipoProducto", Parametros).EjecutarDevuelveReturnValue()) > 0;
        }

        public bool ExisteCodigoReferencia(string CodigoReferencia, int IdTipoProducto) {
            List<TipoProductoDTO> Resultado = Consultar(string .Empty ,CodigoReferencia ,IdTipoProducto);
            return Resultado.Count > 0;
        }


        #endregion

    }
}
