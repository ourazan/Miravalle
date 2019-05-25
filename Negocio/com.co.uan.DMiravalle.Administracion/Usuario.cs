
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
    
        private string Clave;

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Correo { get; set; }

        public Sede SedeAsignada { get; set; }

        public int IdUsuario { get; set; }
    
        public string NombreUsuario { get; set; }

        public int Perfil { get; set; }

        public int UsuarioAutenticado { get; set; }
        #endregion
        #region Constructores
        public Usuario() { }
        public Usuario(int IdUsuario) {
            List<Usuario> Registro = Consultar( IdUsuario,string .Empty ,string.Empty ,0,string .Empty ,0);
            this.IdUsuario = IdUsuario;
            if (Registro.Count > 0) {
                Nombre = Registro[0].Nombre;
                Apellido = Registro[0].Apellido;
                Correo = Registro[0].Correo;
                SedeAsignada = Registro[0].SedeAsignada;
                
                NombreUsuario = Registro[0].NombreUsuario;
                Clave = Registro[0].Clave;
                Perfil = Registro[0].Perfil;
            }
        }
        public Usuario(string nombre, string apellido, string correo, Sede sedeAsignada, int idUsuario, int idSede, string nombreUsuario, string clave,int perfil)
        {
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            SedeAsignada = sedeAsignada;
            IdUsuario= idUsuario;
            NombreUsuario = nombreUsuario;
            Clave = clave;
            Perfil = perfil;
        }
        #endregion
        #region Metodos
        public List<Usuario> Consultar(int IdUsuario, string Nombre, string Apellido, int IdSede, string Correo, int Perfil)
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

            List<Usuario> Resultado = ((from fila in Coleccion.AsEnumerable()
                                        select new Usuario(
                                             fila["Nombre"].ToString()
                                             ,fila["Apellido"].ToString()
                                             , fila["Correo"].ToString()
                                             , new Sede(fila["NombreSede"].ToString()
                                                        ,fila["Direccion"].ToString()
                                                        ,fila["Ciudad"].ToString()
                                                        ,Int32.Parse(fila["IdSede"].ToString())
                                                        ,new Usuario(fila["Administrador_Nombre"].ToString()
                                                                     ,fila["Administrador_Apellido"].ToString()
                                                                     ,fila["Administrador_Correo"].ToString()
                                                                     ,new Sede()
                                                                     ,0,0, fila["Administrador_Usuario"].ToString()
                                                                     , fila["Administrador_Clave"].ToString()
                                                                     ,Convert.ToInt32(fila["Administrador_Perfil"]) 
                                                                     )
                                                       )
                                             ,Int32.Parse(fila["IdUsuario"].ToString())
                                             ,Int32.Parse( fila["IdSede"].ToString())
                                             ,fila["NombreUsuario"].ToString()
                                             ,fila["Clave"].ToString()
                                             , Convert.ToInt32(fila["Perfil"])
                                             )
                                             ).ToList() );


            return Resultado;

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
                ,new Parametros("@UsuarioAutenticado",this.UsuarioAutenticado,SqlDbType.Int,ParameterDirection.Input)
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
                ,new Parametros("@Clave",Clave==""?DBNull.Value:(object)Clave,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@IdSede",(IdSede==0?DBNull.Value:(object)IdSede),SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@IdUsuario",IdUsuario,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@Perfil",Perfil,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@UsuarioAutenticado",this.UsuarioAutenticado,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@RETURN_VALUE",null,SqlDbType.Int,ParameterDirection.ReturnValue)
            };
            return Convert.ToInt32(new Transaccion("EditarUsuario", Parametros).EjecutarDevuelveReturnValue()) > 0;
        }
        public bool Eliminar(int IdUsuario)
        {
            List<Parametros> Parametros = new List<Parametros>() {
               new Parametros("@IdUsuario",IdUsuario,SqlDbType.Int,ParameterDirection.Input)
               ,new Parametros("@UsuarioAutenticado",this.UsuarioAutenticado,SqlDbType.Int,ParameterDirection.Input)
               ,new Parametros("@RETURN_VALUE",null,SqlDbType.Int,ParameterDirection.ReturnValue)
            };
            return Convert.ToInt32(new Transaccion("EliminarUsuario", Parametros).EjecutarDevuelveReturnValue()) > 0;
        }

       
        #endregion
    }
}
