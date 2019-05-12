using System.Collections.Generic;

namespace com.co.uan.DMiravalle.Inventario
{
  public  interface IProducto
    {

        List<Producto> Consultar(string Filtro);

        bool Crear(string Nombre, int IdTipoProducto);

        bool Editar(string Nombre, int IdTipoProducto, int IdProducto);

        bool Eliminar(int IdProducto);

    }
}
