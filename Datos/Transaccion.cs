using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
   public  class Transaccion
    {
        #region CONSTRUCTORES
       
        public Transaccion(string procedimientoAlmacenado, List<Parametros> listaParametros)
        {
            this.ProcedimientoAlmacenado = procedimientoAlmacenado;
            this.ListaParametros = listaParametros;
        }
        #endregion
        #region PROPIEDADES 
        enum EjecutarConsulta { DevuelveTabla, DevuelveColeccionTablas, DevuelveListaClaseT, DevuelveReturnValue, DevuelveParametrosOutput, DevuelveRespuestaDataBase }
        string RutaConexion
        {
            get
            {
               string CadenaConexion = String.Empty;
               CadenaConexion = ConfigurationManager.ConnectionStrings["DMiravalleConexion"].ConnectionString;
               return CadenaConexion;
            }
        }

        List<Parametros> ListaParametros { get; set; }
     
        string ProcedimientoAlmacenado { get; set; }
               
        int Respuesta { get; set; }

       
       
        EjecutarConsulta Ejecutar { get; set; }

        DataTable DevuelveTabla { get; set; }

        public object DevuelveReturnValue { get; set; }

        public Dictionary<int, string> respuestasDataBase { get; set; }

        #endregion
        #region METODOS
        public DataTable EjecutarDevuelveTabla()
        {
            this.Ejecutar = EjecutarConsulta.DevuelveTabla;
            CRUD();
            return this.DevuelveTabla;
        }
    
        public object EjecutarDevuelveReturnValue()
        {
            this.Ejecutar = EjecutarConsulta.DevuelveReturnValue;
            CRUD();
            return this.DevuelveReturnValue;
        }

        public Dictionary<int, string> EjecutarDevuelveRespuestaDataBase()
        {
            this.Ejecutar = EjecutarConsulta.DevuelveRespuestaDataBase;
            CRUD();
            return this.respuestasDataBase;
        }
        void CRUD()
        {
            SqlDataAdapter newSqlDataAdapter;
            SqlConnection newSqlConnection = new SqlConnection(RutaConexion);
            newSqlConnection.Open();
            SqlCommand newSqlCommand = new SqlCommand();
            newSqlCommand.Connection = newSqlConnection;
            newSqlCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter newSqlParameter;
            try
            {
                newSqlCommand.CommandText = this.ProcedimientoAlmacenado;
                foreach (Parametros item in this.ListaParametros)
                {
                    newSqlParameter = new SqlParameter();
                    newSqlParameter.ParameterName = item.Nombre;
                    newSqlParameter.SqlDbType = item.TipoDato;
                    newSqlParameter.SqlValue = item.Valor;
                    newSqlParameter.Direction = item.Sentido;
                    newSqlParameter.Size = 10000000;
                    newSqlCommand.Parameters.Add(newSqlParameter);
                }
                switch (this.Ejecutar)
                {
                    case EjecutarConsulta.DevuelveTabla:
                        this.DevuelveTabla = new DataTable();
                        newSqlDataAdapter = new SqlDataAdapter(newSqlCommand);
                        newSqlDataAdapter.Fill(this.DevuelveTabla);
                        break;
                 
                    case EjecutarConsulta.DevuelveReturnValue:
                        newSqlCommand.ExecuteNonQuery();
                        this.DevuelveReturnValue = newSqlCommand.Parameters["@RETURN_VALUE"].Value;
                        this.Respuesta = (int)this.DevuelveReturnValue;
                        break;
                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                newSqlConnection.Close();
            }
           
        }
        #endregion
    }
}
