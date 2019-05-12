using System.Collections.Generic;

namespace com.co.uan.DMiravalle.Administracion
{
   public  interface IGerente
    {


        List<Usuario> Consultar(string Filtro);

        bool Agregar(string Nombre, string Apellido, int IdSede, string Correo, string Usuario, string Clave);

        bool Editar(int IdUsuario, string Nombre, string Apellido, int IdSede, string Correo, string Clave);

        bool Eliminar(int IdUsuario);
    
    }
}
