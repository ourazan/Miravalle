﻿using com.co.uan.DMiravalle.Inventario;
using System.Collections.Generic;
using System.Data;

namespace com.co.uan.DMiravalle.Informes
{ 

  public  interface INotificacionVencidos
    {

        List<InventarioDTO> ConsultarProductosVencidos(string Filtro);
        DataTable ConsultarProductosVencidosTabla(string Filtro);

    }
}
