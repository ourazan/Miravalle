using System.Collections.Generic;
using System.Data;
using Datos;
using System;
using System.Linq;


namespace com.co.uan.DMiravalle.Inventario
{
  public   class Producto: IProducto
    {
        #region Propiedades
        private int usuarioautenticado;
        public int UsuarioAutenticado
        {
            set { usuarioautenticado = value; }
        }

        #endregion

        #region Metodos


        public List<ProductoDTO> Consultar(string Nombre, int IdTipoProducto, int IdProducto)
        {
            List<Parametros> Parametros = new List<Parametros>() {
                new Parametros("@NombreProducto",string.IsNullOrEmpty (Nombre)?DBNull.Value :(object)Nombre,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@IdTipoProducto",IdTipoProducto==0 ? DBNull.Value : (object)IdTipoProducto, SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@IdProducto",IdProducto==0 ? DBNull.Value : (object)IdProducto,SqlDbType.Int,ParameterDirection.Input)
            };
            DataTable Coleccion = new Transaccion("ConsultarProducto", Parametros).EjecutarDevuelveTabla();
            List<ProductoDTO> Resultado =(from fila in Coleccion.AsEnumerable()
                                            select new ProductoDTO( fila["NombreProducto"].ToString()
                                                                    ,Convert.ToInt32 ( fila["IdProducto"])
                                                                    ,new TipoProductoDTO(Convert.ToInt32(fila["IdTipoProducto"])
                                                                                    , fila["CodigoReferencia"].ToString()
                                                                                    , fila["Descripcion"].ToString())
                                                            )).ToList();


            return Resultado;
        }

        public bool Crear(string Nombre, int IdTipoProducto)
        {
            List<Parametros> Parametros = new List<Parametros>() {
                new Parametros("@NombreProducto",Nombre,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@IdTipoProducto",IdTipoProducto,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@UsuarioAutenticado",usuarioautenticado,SqlDbType.Int,ParameterDirection.Input)
                 ,new Parametros("@RETURN_VALUE",null,SqlDbType.Int,ParameterDirection.ReturnValue)
            };
            return Convert.ToInt32(new Transaccion("CrearProducto", Parametros).EjecutarDevuelveReturnValue()) > 0;
        }

        public bool Editar(string Nombre, int IdTipoProducto, int IdProducto)
        {
            List<Parametros> Parametros = new List<Parametros>() {
                new Parametros("@NombreProducto",Nombre,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@IdTipoProducto",IdTipoProducto,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@UsuarioAutenticado",this.usuarioautenticado,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@IdProducto",IdProducto,SqlDbType.Int,ParameterDirection.Input)
                 ,new Parametros("@RETURN_VALUE",null,SqlDbType.Int,ParameterDirection.ReturnValue)
            };
            return Convert.ToInt32(new Transaccion("EditarProducto", Parametros).EjecutarDevuelveReturnValue()) > 0;
        }

        public bool Eliminar(int IdProducto)
        {
            List<Parametros> Parametros = new List<Parametros>() {
                new Parametros("@UsuarioAutenticado",this.usuarioautenticado,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@IdProducto",IdProducto,SqlDbType.Int,ParameterDirection.Input)
                 ,new Parametros("@RETURN_VALUE",null,SqlDbType.Int,ParameterDirection.ReturnValue)
            };
            return Convert.ToInt32(new Transaccion("EliminarProducto", Parametros).EjecutarDevuelveReturnValue()) > 0;
        }
      
        #endregion
    }
}
