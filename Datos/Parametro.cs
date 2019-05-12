using System.Data;

namespace Datos
{
    public class Parametros
    {
        #region Propiedades

        internal string Nombre { get; set; }
        internal object Valor { get; set; }
        internal SqlDbType TipoDato { get; set; }
        internal ParameterDirection Sentido { get; set; }

        #endregion

        #region Costructores
        public Parametros(string nombre, object valor, SqlDbType tipoDato, ParameterDirection sentido)
        {
            this.Nombre = nombre;
            this.Valor = valor;
            this.TipoDato = tipoDato;
            this.Sentido = sentido;
        }
        #endregion

    }
}
