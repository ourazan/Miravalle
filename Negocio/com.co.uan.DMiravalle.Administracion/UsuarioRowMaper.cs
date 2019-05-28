using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace com.co.uan.DMiravalle.Administracion
{
   public  class UsuarioRowMaper
    {
        public List<UsuarioDTO> MapearDatos(DataTable Coleccion) {
         return    ((from fila in Coleccion.AsEnumerable()
              select new UsuarioDTO(
                   fila["Nombre"].ToString()
                   , fila["Apellido"].ToString()
                   , fila["Correo"].ToString()
                   , new SedeDTO(fila["NombreSede"].ToString()
                              , fila["Direccion"].ToString()
                              , fila["Ciudad"].ToString()
                              , Convert.ToInt32(fila["IdSede"].ToString())
                              , new UsuarioDTO(fila["Administrador_Nombre"].ToString()
                                           , fila["Administrador_Apellido"].ToString()
                                           , fila["Administrador_Correo"].ToString()
                                           , new SedeDTO()
                                           , 0, 0, fila["Administrador_Usuario"].ToString()
                                           , fila["Administrador_Clave"].ToString()
                                           , Convert.ToInt32(fila["Administrador_Perfil"])
                                           )
                             )
                   , Convert.ToInt32(fila["IdUsuario"].ToString())
                   , Convert.ToInt32(fila["IdSede"].ToString())
                   , fila["NombreUsuario"].ToString()
                   , fila["Clave"].ToString()
                   , Convert.ToInt32(fila["Perfil"])
                   )).ToList());
        }
    }
}
