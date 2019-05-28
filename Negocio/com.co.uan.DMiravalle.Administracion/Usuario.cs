
using com.co.uan.DMiravalle.Funcionales;
using Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace com.co.uan.DMiravalle.Administracion
{
   public  class Usuario :  IGerente
    {
        #region Propiedades
        UsuarioRowMaper Maper;
        private int usuarioautenticado;
        public int UsuarioAutenticado
        {
            set { usuarioautenticado = value; }
        }
        #endregion
        public Usuario(){
            Maper= new UsuarioRowMaper();
       }

        #region Metodos
        public List<UsuarioDTO> Consultar(int IdUsuario, string Nombre, string Apellido, int IdSede, string Correo, int Perfil)
        {
            List<Parametros> Parametros = new List<Parametros>() {
                 new Parametros("@Nombre",string.IsNullOrEmpty(Nombre)?DBNull .Value :(object)Nombre,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@Apellido",string.IsNullOrEmpty(Apellido) ? DBNull.Value : (object)Apellido,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@Correo",string.IsNullOrEmpty(Correo) ? DBNull.Value : (object)Correo,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@IdSede",(IdSede==0?DBNull.Value:(object)IdSede),SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@IdUsuario",(IdUsuario==0?DBNull.Value:(object)IdUsuario),SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@Perfil",(Perfil == 0 ? DBNull.Value : (object)Perfil),SqlDbType.Int,ParameterDirection.Input)
            };
            DataTable Coleccion = new Transaccion("ConsultarUsuario",Parametros).EjecutarDevuelveTabla();
          return   Maper.MapearDatos(Coleccion);
        }
        public bool Agregar(string Nombre, string Apellido, int IdSede, string Correo, string usuario, string Clave,int Perfil)
        {
             List<Parametros> Parametros = new List<Parametros>() {
                new Parametros("@Nombre",Nombre,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@Apellido",Apellido,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@Correo",Correo,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@Clave",Clave,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@NombreUsuario",usuario,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@IdSede",(IdSede==0?DBNull.Value:(object)IdSede),SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@UsuarioAutenticado",usuarioautenticado,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@Perfil",Perfil,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@RETURN_VALUE",null,SqlDbType.Int,ParameterDirection.ReturnValue)
            };
            return Convert.ToInt32(new Transaccion("CrearUsuario", Parametros).EjecutarDevuelveReturnValue())>0;
        }
        public bool Editar(int IdUsuario, string Nombre, string Apellido, int IdSede, string Correo, string Clave, int Perfil)
        {
                List<Parametros> Parametros = new List<Parametros>() {
                 new Parametros("@Nombre",Nombre,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@Apellido",Apellido,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@Correo",Correo,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@Clave",string.IsNullOrEmpty ( Clave)?DBNull.Value:(object)Clave,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@IdSede",(IdSede==0?DBNull.Value:(object)IdSede),SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@IdUsuario",IdUsuario,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@Perfil",Perfil,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@UsuarioAutenticado",usuarioautenticado,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@RETURN_VALUE",null,SqlDbType.Int,ParameterDirection.ReturnValue)
            };
            return Convert.ToInt32(new Transaccion("EditarUsuario", Parametros).EjecutarDevuelveReturnValue()) > 0;
        }
        public bool Eliminar(int IdUsuario)
        {
            List<Parametros> Parametros = new List<Parametros>() {
               new Parametros("@IdUsuario",IdUsuario,SqlDbType.Int,ParameterDirection.Input)
               ,new Parametros("@UsuarioAutenticado",usuarioautenticado,SqlDbType.Int,ParameterDirection.Input)
               ,new Parametros("@RETURN_VALUE",null,SqlDbType.Int,ParameterDirection.ReturnValue)
            };
            return Convert.ToInt32(new Transaccion("EliminarUsuario", Parametros).EjecutarDevuelveReturnValue()) > 0;
        }

        public UsuarioDTO Validar(string Usuario, string Clave)
        {
            List<Parametros> Parametros = new List<Parametros>()
            {
                new Parametros("@NombreUsuario",string.IsNullOrEmpty(Usuario) ? DBNull.Value : (object)Usuario,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@Clave",Encripcion.CodificarSHA256(Clave),SqlDbType.VarChar,ParameterDirection.Input)
            };
            DataTable Coleccion = new Transaccion("ValidarUsuario", Parametros).EjecutarDevuelveTabla();
            return Maper.MapearDatos(Coleccion).FirstOrDefault();
        }

        #endregion
    }
}
