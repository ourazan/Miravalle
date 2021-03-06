﻿
using System.Collections.Generic;

namespace com.co.uan.DMiravalle.Administracion
{
   public  class FachadaAdministracion:IFachadaAdministracion
    {
        #region Propiedades
           private ISede Sede;

        private IGerente Gerente;
        #endregion

        #region Constructores
        public FachadaAdministracion( ) {
          
            this.Gerente = new Usuario();
            this.Sede =new Sede();
        }
        #endregion

        #region Metodos
        public List<UsuarioDTO> ConsultarUsuario(int IdUsuario, string Nombre, string Apellido, int IdSede, string Correo, int Perfil)
        {
            return Gerente.Consultar( IdUsuario,  Nombre,  Apellido,  IdSede,  Correo,  Perfil);
        }
        public bool CrearSede(string Nombre, string Ciudad, string Direccion,int IdAdministrador)
        {
           return   Sede.Crear(Nombre,  Ciudad,  Direccion,  IdAdministrador);
        }
        public bool CrearUsuario(string Nombre, string Apellido, int IdSede, string Correo,string Usuario, string Clave, int Perfil)
        {
            return Gerente.Agregar(Nombre,  Apellido,  IdSede,  Correo,  Usuario,  Clave,  Perfil);
        }
        public bool EditarSede(string Nombre, string Ciudad, string Direccion,int IdAdministrador, int IdSede)
        {
            return Sede.Editar( Nombre,  Ciudad,  Direccion,  IdAdministrador,  IdSede);
        }
        public bool EditarUsuario(int IdUsuario, string Nombre, string Apellido, int IdSede, string Correo,string Clave, int Perfil)
        {
            return Gerente.Editar( IdUsuario,  Nombre,  Apellido,  IdSede,  Correo, Clave,  Perfil);
        }
        public bool EliminarSede(int IdSede)
        {
            return Sede.Eliminar(IdSede);
        }
        public bool RemoverUsuario(int IdUsuario)
        {
            return Gerente.Eliminar(IdUsuario);
        }

        public List<SedeDTO> ConsultarSede(string Nombre, string Ciudad, string Direccion, int IdAdministrador, int IdSed)
        {
            return Sede.Consultar( Nombre,  Ciudad,  Direccion,  IdAdministrador,  IdSed);
        }
        public void AsignarEjecutor(int Autenticado) {
            if (this.Gerente!=null)
                ((Usuario)Gerente).UsuarioAutenticado = Autenticado;
            if (this.Sede != null)
                 ((Sede)Sede).UsuarioAutenticado = Autenticado;

        }
        public UsuarioDTO validarUsuario(string Usuario, string Clave)
        {
            return Gerente.Validar(Usuario, Clave);
        }
        #endregion
    }
}
