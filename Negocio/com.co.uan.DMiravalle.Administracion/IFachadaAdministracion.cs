
using com.co.uan.DMiravalle.Administracion;
using System.Collections.Generic;

namespace com.co.uan.DMiravalle.Administracion
{
  public   interface IFachadaAdministracion
    {

        List<Usuario> ConsultarUsuario(string Filtro);
        List<Sede>ConsultarSede(string Filtro);

        bool CrearSede(string Nombre, string Ciudad, string Direccion, int IdAdministrador);

        bool CrearUsuario(string Nombre, string Apellido, int IdSede, string Correo, string Usuario, string Clave);

        bool EditarUsuario(int IdUsuario, string Nombre, string Apellido, int IdSede, string Correo, string Clave);

        bool EditarSede(string Nombre, string Ciudad, string Direccion, int IdAdministrador, int IdSede);

        bool EliminarSede(int IdSede);

        bool RemoverUsuario(int IdUsuario);
        void AsignarEjecutor(int Autenticado);
    }
}
