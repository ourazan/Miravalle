using System.Collections.Generic;
using System.Data;
using Datos;
using System;
using System.Linq;
using Negocio;

namespace com.co.uan.DMiravalle.Administracion
{
    public class Sede : Ejecutor,ISede
    {
       

        public  Usuario Administrador { get; set; }

        public string NombreSede { get; set; }

        public string Direccion { get; set; }

        public string Ciudad { get; set; }

        public int IdSede { get; set; }

        public Sede() {

        }
       
        public Sede(int idSede) {
            this.IdSede = IdSede;
           
            List<Sede>Resultado= Consultar("IdSede="+IdSede);
            if (Resultado.Count>0)
            {
                Administrador = Resultado[0].Administrador;
                Direccion = Resultado[0].Direccion;
                Ciudad = Resultado[0].Ciudad;
                IdSede = Resultado[0].IdSede;
            }
        }      
        public Sede( string direccion, string ciudad, int idSede, Usuario administrador)
        {
            Administrador = administrador;
            Direccion = direccion;
            Ciudad = ciudad;
            IdSede = idSede;
         
        }

      
        public List<Sede> Consultar(string Filtro)
        {
            List<Parametros> Parametros = new List<Parametros>() {
                new Parametros("@filtro",Filtro,SqlDbType.VarChar,ParameterDirection.Input)
            };
            DataTable Coleccion = new Transaccion("ConsultarSede", Parametros).EjecutarDevuelveTabla();
            List<Sede> Resultado = (from fila in Coleccion.AsEnumerable()
                                        select new Sede(fila["Direccion"].ToString()
                                                        , fila["Ciudad"].ToString()
                                                        , Int32.Parse(fila["IdSede"].ToString())
                                                        , new Usuario(fila["Nombre"].ToString()
                                                                     , fila["Apellido"].ToString()
                                                                     , fila["Correo"].ToString()
                                                                     , new Sede()
                                                                     , Int32.Parse(fila["IdAdministrador"].ToString())
                                                                     ,  0
                                                                     , fila["NombreUsuario"].ToString()
                                                                     , fila["Clave"].ToString()
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
                ,new Parametros("@UsuarioAutenticado",UsuarioAutenticado,SqlDbType.Int,ParameterDirection.Input)
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
                ,new Parametros("@UsuarioAutenticado",this.UsuarioAutenticado,SqlDbType.Int,ParameterDirection.Input)
                 ,new Parametros("@RETURN_VALUE",null,SqlDbType.Int,ParameterDirection.ReturnValue)
            };
            return Convert.ToInt32(new Transaccion("EditarSede", Parametros).EjecutarDevuelveReturnValue()) > 0;
        }

        public bool Eliminar(int IdSede)
        {
               List<Parametros> Parametros = new List<Parametros>() {
                new Parametros("@IdSede",IdSede,SqlDbType.Int,ParameterDirection.Input)
                ,new Parametros("@UsuarioAutenticado",this.UsuarioAutenticado,SqlDbType.Int,ParameterDirection.Input)
                 ,new Parametros("@RETURN_VALUE",null,SqlDbType.Int,ParameterDirection.ReturnValue)
                };
                return Convert.ToInt32(new Transaccion("EliminarSede", Parametros).EjecutarDevuelveReturnValue()) > 0;
        }
       
    }
}
