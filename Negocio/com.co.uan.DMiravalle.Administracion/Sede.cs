using System.Collections.Generic;
using System.Data;
using Datos;
using System;
using System.Linq;


namespace com.co.uan.DMiravalle.Administracion
{
    public class Sede :ISede
    {
        private int usuarioautenticado;
        public int UsuarioAutenticado {
            set{ usuarioautenticado = value; }
        }

     
        public List<SedeDTO> Consultar(string Nombre, string Ciudad, string Direccion, int IdAdministrador, int IdSede)
        {
            List<Parametros> Parametros = new List<Parametros>() {
                new Parametros("@NombreSede",String.IsNullOrEmpty (Nombre)? DBNull.Value:(object)Nombre,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@Ciudad",String.IsNullOrEmpty (Ciudad )? DBNull.Value : (object)Ciudad, SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@Direccion",String.IsNullOrEmpty (Direccion )? DBNull.Value : (object)Direccion, SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@IdAdministrador",(IdAdministrador==0?(object)DBNull.Value:(object)IdAdministrador),SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@IdSede",(IdSede==0?(object)DBNull.Value:(object)IdSede),SqlDbType.Int,ParameterDirection.Input)
            };
            DataTable Coleccion = new Transaccion("ConsultarSede", Parametros).EjecutarDevuelveTabla();
            List<SedeDTO> Resultado = (from fila in Coleccion.AsEnumerable()
                                        select new SedeDTO(fila["NombreSede"].ToString()
                                                        , fila["Direccion"].ToString()
                                                        , fila["Ciudad"].ToString()
                                                        , Int32.Parse(fila["IdSede"].ToString())
                                                        , new UsuarioDTO(fila["Nombre"].ToString()
                                                                     , fila["Apellido"].ToString()
                                                                     , fila["Correo"].ToString()
                                                                     , new SedeDTO()
                                                                     , Int32.Parse(fila["IdAdministrador"].ToString())
                                                                     ,  0
                                                                     , fila["NombreUsuario"].ToString()
                                                                     , fila["Clave"].ToString()
                                                                     ,Convert.ToInt32(fila["Perfil"])
                                                                     )
                                                       )
                                                       ).ToList();


            return Resultado;
        }

        public bool Crear(string Nombre, string Ciudad, string Direccion, int IdAdministrador)
        {
            List<Parametros> Parametros = new List<Parametros>() {
                new Parametros("@NombreSede",Nombre,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@Ciudad",Ciudad,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@Direccion",Direccion,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@IdAdministrador",(IdAdministrador==0?(object)DBNull.Value:(object)IdAdministrador),SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@UsuarioAutenticado",usuarioautenticado,SqlDbType.Int,ParameterDirection.Input)
                 ,new Parametros("@RETURN_VALUE",null,SqlDbType.Int,ParameterDirection.ReturnValue)
            };
            return Convert.ToInt32(new Transaccion("CrearSede", Parametros).EjecutarDevuelveReturnValue()) > 0;
        }

        public bool Editar(string Nombre, string Ciudad, string Direccion, int IdAdministrador, int IdSede)
        {
            List<Parametros> Parametros = new List<Parametros>() {
                new Parametros("@NombreSede",Nombre,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@Ciudad",Ciudad,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@Direccion",Direccion,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@IdAdministrador",(IdAdministrador==0?(object)DBNull.Value:(object)IdAdministrador),SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@IdSede",IdSede,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@UsuarioAutenticado",usuarioautenticado,SqlDbType.Int,ParameterDirection.Input)
                 ,new Parametros("@RETURN_VALUE",null,SqlDbType.Int,ParameterDirection.ReturnValue)
            };
            return Convert.ToInt32(new Transaccion("EditarSede", Parametros).EjecutarDevuelveReturnValue()) > 0;
        }

        public bool Eliminar(int IdSede)
        {
               List<Parametros> Parametros = new List<Parametros>() {
                new Parametros("@IdSede",IdSede,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@UsuarioAutenticado",usuarioautenticado,SqlDbType.Int,ParameterDirection.Input)
                 ,new Parametros("@RETURN_VALUE",null,SqlDbType.Int,ParameterDirection.ReturnValue)
                };
                return Convert.ToInt32(new Transaccion("EliminarSede", Parametros).EjecutarDevuelveReturnValue()) > 0;
        }
       
    }
}
