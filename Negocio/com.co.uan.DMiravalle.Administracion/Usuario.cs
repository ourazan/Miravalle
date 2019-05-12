
using Datos;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace com.co.uan.DMiravalle.Administracion
{
   public  class Usuario : Ejecutor, IGerente
    {
        #region Propiedades
    
        private string Clave;

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Correo { get; set; }

        public Sede SedeAsignada { get; set; }

        public int IdUsuario { get; set; }
    
        public string NombreUsuario { get; set; }

        #endregion
        #region Constructores
        public Usuario() { }
        public Usuario(int IdUsuario) {
            List<Usuario> Registro = Consultar(" idUsuario=" + IdUsuario.ToString());
            this.IdUsuario = IdUsuario;
            if (Registro.Count > 0) {
                Nombre = Registro[0].Nombre;
                Apellido = Registro[0].Apellido;
                Correo = Registro[0].Correo;
                SedeAsignada = Registro[0].SedeAsignada;
                
                NombreUsuario = Registro[0].NombreUsuario;
                Clave = Registro[0].Clave;
               
            }
        }
        public Usuario(string nombre, string apellido, string correo, Sede sedeAsignada, int idUsuario, int idSede, string nombreUsuario, string clave)
        {
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            SedeAsignada = sedeAsignada;
            IdUsuario= idUsuario;
            NombreUsuario = nombreUsuario;
            Clave = clave;
        }
        #endregion
        #region Metodos
        public List<Usuario> Consultar(string Filtro)
        {
            List<Parametros> Parametros = new List<Parametros>() {
                new Parametros("@filtro",Filtro,SqlDbType.VarChar,ParameterDirection.Input)
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
                                                                     ,0,0, fila["Administrador_NombreUsuario"].ToString()
                                                                     , fila["Administrador_Clave"].ToString()
                                                                     )
                                                       )
                                             ,Int32.Parse(fila["IdUsuario"].ToString())
                                             ,Int32.Parse( fila["IdSede"].ToString())
                                             ,fila["NombreUsuario"].ToString()
                                             ,fila["Clave"].ToString()
                                             )
                                             ).ToList() );


            return Resultado;

        }
        public bool Agregar(string Nombre, string Apellido, int IdSede, string Correo, string usuario, string Clave)
        {
             List<Parametros> Parametros = new List<Parametros>() {
                new Parametros("@Nombre",Nombre,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@Apellido",Apellido,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@Correo",Correo,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@Clave",Clave,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@IdSede",IdSede,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@UsuarioAutenticado",this.UsuarioAutenticado,SqlDbType.Int,ParameterDirection.Input)
                 ,new Parametros("@RETURN_VALUE",null,SqlDbType.Int,ParameterDirection.ReturnValue)
            };
            return Convert.ToInt32(new Transaccion("CrearUsuario", Parametros).EjecutarDevuelveReturnValue())>0;
        }
        public bool Editar(int IdUsuario, string Nombre, string Apellido, int IdSede, string Correo, string Clave)
        {
                List<Parametros> Parametros = new List<Parametros>() {
                 new Parametros("@Nombre",Nombre,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@Apellido",Apellido,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@Correo",Correo,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@Clave",Clave,SqlDbType.VarChar,ParameterDirection.Input)
                ,new Parametros("@IdSede",IdSede,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@IdUsuario",IdUsuario,SqlDbType.Int,ParameterDirection.Input)
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
