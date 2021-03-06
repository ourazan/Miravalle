﻿
using System.Collections.Generic;

namespace com.co.uan.DMiravalle.Administracion
{
  public   interface IFachadaAdministracion
    {

        List<UsuarioDTO> ConsultarUsuario(int IdUsuario, string Nombre, string Apellido, int IdSede, string Correo, int Perfil);
        List<SedeDTO>ConsultarSede(string Nombre, string Ciudad, string Direccion, int IdAdministrador, int IdSed);

        bool CrearSede(string Nombre, string Ciudad, string Direccion, int IdAdministrador);

        bool CrearUsuario(string Nombre, string Apellido, int IdSede, string Correo, string Usuario, string Clave, int Perfil);

        bool EditarUsuario(int IdUsuario, string Nombre, string Apellido, int IdSede, string Correo, string Clave, int Perfil);

        bool EditarSede(string Nombre, string Ciudad, string Direccion, int IdAdministrador, int IdSede);

        bool EliminarSede(int IdSede);

        bool RemoverUsuario(int IdUsuario);
        void AsignarEjecutor(int Autenticado);

        UsuarioDTO validarUsuario(string Usuario, string Clave);
    }
}
