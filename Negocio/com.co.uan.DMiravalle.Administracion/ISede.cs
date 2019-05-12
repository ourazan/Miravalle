using System.Collections.Generic;

namespace com.co.uan.DMiravalle.Administracion
{
  public   interface ISede
    {

        List<Sede> Consultar(string Filtro);

        bool Crear(string Nombre, string Ciudad, string Direccion, int IdAdministrador);

        bool Editar(string Nombre, string Ciudad, string Direccion, int IdAdministrador, int IdSede);

        bool Eliminar(int IdSede);
    }
}
