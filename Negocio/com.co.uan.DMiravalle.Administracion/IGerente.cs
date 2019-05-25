using System.Collections.Generic;

namespace com.co.uan.DMiravalle.Administracion
{
   public  interface IGerente
    {


        List<Usuario> Consultar(int IdUsuario, string Nombre, string Apellido, int IdSede, string Correo, int Perfil);

        bool Agregar(string Nombre, string Apellido, int IdSede, string Correo, string Usuario, string Clave,int Perfil);

        bool Editar(int IdUsuario, string Nombre, string Apellido, int IdSede, string Correo, string Clave, int Perfil);

        bool Eliminar(int IdUsuario);
    
    }
}
