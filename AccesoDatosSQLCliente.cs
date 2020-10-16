using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Data;

using System.Collections;


using System.Configuration;

/// <summary>
///Clase genérica de acceso a datos
public abstract class AccesoDatos
{
    private string _CadenaConexion;
    private string _ConsultaSQL;
    protected ArrayList _NombreParametros = new ArrayList();
    protected ArrayList _ValorParametros = new ArrayList();

    //Cadena de conexion a la base de datos.
    public string CadenaConexion
    {
        get
        {
            return (_CadenaConexion);
        }
        set
        {
            _CadenaConexion = value;
        }
    }

    //Consulta que se va a ejecutar
    public string ConsultaSQL
    {
        get
        {
            return (_ConsultaSQL);
        }
        set
        {
            _ConsultaSQL = value;
        }
    }

    //-----------------------------------------------------------------------------
    //Funcion:AñadirParametro
    //Descripcion:Añade a los arrays de parametros de sql , el parametro sql  pasado por parametro
    //No se debe especificar ningun simbolo del tipo @ -> para el nombre del parametro, puesto
    //que cada conexion sabra como trabajar con sus parametros

    //-----------------------------------------------------------------------------

    public void AñadirParametro(string sNombreParametro, string sValorParametro)
    {
        this._NombreParametros.Add(sNombreParametro);
        this._ValorParametros.Add(sValorParametro);
    }


    //public abstract System.Data.DataSet EjecucionConsulta(bool bTiparDataset)
    //{


    //}

}



/// <summary>
/// Descripción breve de AccesoDatosSQLCliente
/// </summary>
public class AccesoDatosSQLCliente : AccesoDatos
{

    #region "Variables privadas "
    //Ejecuta una instrucción sql, obteniendo en un dataset el contenido de la    //consulta
    private int nTiempoEjecucionConsulta = -1;
   // private System.Data.SqlClient.SqlParameter[] _parametros;
    #endregion

    #region " Constructores "
    //-----------------------------------------------------------------------------
    //Funcion: New
    //Parámetro: cadenaConexion (System.String)
    //Parámetro: consultaSQL (System.String)
    //Establece la cadena de conexión y la consulta
    //-----------------------------------------------------------------------------
    public AccesoDatosSQLCliente(string cadenaConexion, string consultaSQL)
    {
        EstablecerCadenaConexion(cadenaConexion);
        EstablecerConsultaSQL(consultaSQL);
    }

    //-----------------------------------------------------------------------------
    //Funcion: New
    //Parámetro: cadenaConexion (System.String)
    //Parámetro: consultaSQL (System.String)
    //Parámetro: tiempoEjecucionConsulta (System.Int32)
    //Establece la cadena de conexión y la consulta y el tiempo de ejecución de la consulta. Este tiempo
    //   es el maximo que puede tardar ya que si no de cancela la consulta
    //-----------------------------------------------------------------------------
    public AccesoDatosSQLCliente(string cadenaConexion, string consultaSQL, int tiempoEjecucionConsulta)
    {
        EstablecerCadenaConexion(cadenaConexion);
        EstablecerConsultaSQL(consultaSQL);
        this.nTiempoEjecucionConsulta = tiempoEjecucionConsulta;
    }

    //-----------------------------------------------------------------------------
    //Funcion: New
    //Parámetro: cadenaConexion (System.String)
    //Establece la cadena de conexion.

    //-----------------------------------------------------------------------------
    public AccesoDatosSQLCliente(string cadenaConexion)
    {
        EstablecerCadenaConexion(cadenaConexion);
    }

    //-----------------------------------------------------------------------------
    //Funcion: New
    //Constructor

    //-----------------------------------------------------------------------------
    public AccesoDatosSQLCliente()
    {
        EstablecerCadenaConexion("");
    }
    #endregion

    #region " Ejecución Consulta "
    //-----------------------------------------------------------------------------
    //Funcion: EjecucionConsulta
    //Parámetro: bTiparDataset (System.Boolean)
    //Retorno: (System.Data.DataSet)
    //Descripcion: Ejecuta la consulta a partir de la consultaSQL , cadena de conexión que se le haya pasado.
    //             Si se le pone bTiparDataset, el dataset resultante tiene información del Esquema

    //-----------------------------------------------------------------------------
    //public  System.Data.DataSet EjecucionConsulta(bool bTiparDataset)
    //{
    //    DataSet dsActual = new DataSet();
    //    this.EjecucionConsulta(ref dsActual, bTiparDataset, String.Empty, null);
    //    return dsActual;
    //}

    //-----------------------------------------------------------------------------
    //Funcion: EjecucionConsulta
    //Parámetro: dsActual (System.Data.DataSet)
    //Parámetro: bTiparDataset (System.Boolean)
    //Descripcion: Ejecuta la consulta a partir de la consultaSQL , cadena de conexión que se le haya pasado.
    //             Si se le pone bTiparDataset, el dataset resultante tiene información del Esquema
    //             Rellena los datos resultantes en el dataset pasado

    //-----------------------------------------------------------------------------
    //public void EjecucionConsulta(ref DataSet dsActual, bool bTiparDataset)
    //{
    //    this._EjecucionConsulta(ref dsActual, bTiparDataset, String.Empty, null, CommandType.Text);
    //}

    //-----------------------------------------------------------------------------
    //Funcion: EjecucionConsulta
    //Parámetro: dsActual (System.Data.DataSet)
    //Parámetro: bTiparDataset (System.Boolean)
    //Parámetro: NombreTablaDestino (System.String)
    //Descripcion: Ejecuta la consulta a partir de la consultaSQL , cadena de conexión que se le haya pasado.
    //             Si se le pone bTiparDataset, el dataset resultante tiene información del Esquema
    //             Rellena los datos resultantes en el dataset pasado
    //             Asigna al dataset pasado el nombre de tabla tambien pasado

    //-----------------------------------------------------------------------------
    //public void EjecucionConsulta(ref DataSet dsActual, bool bTiparDataset, string NombreTablaDestino)
    //{
    //    this._EjecucionConsulta(ref dsActual, bTiparDataset, NombreTablaDestino, null, CommandType.Text);
    //}

    //-----------------------------------------------------------------------------
    //Funcion: EjecucionConsulta
    //Parámetro: dsActual (System.Data.DataSet)
    //Parámetro: bTiparDataset (System.Boolean)
    //Parámetro: NombreTablaDestino (System.String)
    //Parámetro: oTransaccion (System.Data.IDbTransaction)
    //Descripcion: Ejecuta la consulta a partir de la consultaSQL , cadena de conexión que se le haya pasado.
    //             Si se le pone bTiparDataset, el dataset resultante tiene información del Esquema
    //             Rellena los datos resultantes en el dataset pasado
    //             Asigna al dataset pasado el nombre de tabla tambien pasado
    //             Si se le pasa la transacción, se obtiene la conexion del otransaccion

    //-----------------------------------------------------------------------------
    //public void EjecucionConsulta(ref DataSet dsActual, bool bTiparDataset, string NombreTablaDestino, System.Data.IDbTransaction oTransaccion)
    //{
    //    this._EjecucionConsulta(ref dsActual, bTiparDataset, NombreTablaDestino, oTransaccion, CommandType.Text);
    //}

    //-----------------------------------------------------------------------------
    //Funcion: EjecucionConsulta
    //Parámetro: dsActual (System.Data.DataSet)
    //Parámetro: bTiparDataset (System.Boolean)
    //Parámetro: NombreTablaDestino (System.String)
    //Parámetro: oTransaccion (System.Data.IDbTransaction)
    //Descripcion: Ejecuta la consulta a partir de la consultaSQL , cadena de conexión que se le haya pasado.
    //             Si se le pone bTiparDataset, el dataset resultante tiene información del Esquema
    //             Rellena los datos resultantes en el dataset pasado
    //             Asigna al dataset pasado el nombre de tabla tambien pasado
    //             Si se le pasa la transacción, se obtiene la conexion del otransaccion
    //-----------------------------------------------------------------------------
    //private void _EjecucionConsulta(ref DataSet dsActual, bool bTiparDataset, string NombreTablaDestino, System.Data.IDbTransaction oTransaccion, System.Data.CommandType Tipoconsulta)
    //{
    //    System.Data.SqlClient.SqlConnection oSqlConnection;
    //    System.Data.SqlClient.SqlDataAdapter oSqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter();
    //    System.Data.SqlClient.SqlCommand oSqlCommand = new System.Data.SqlClient.SqlCommand();

    //    bool bTransaccionCreadaEnLaFuncion = true;

    //    // si se le pasa una transaccion, le asignamos la conexion
    //    if (oTransaccion != null)
    //    {
    //        bTransaccionCreadaEnLaFuncion = false;
    //        oSqlConnection = ((System.Data.SqlClient.SqlConnection)oTransaccion.Connection);
    //    }
    //    else
    //    {
    //        oSqlConnection = new System.Data.SqlClient.SqlConnection();
    //    }

    //    try
    //    {
    //        // Si de antemano, sabemos que no es posible la ejecución lanzamos el error.
    //        if (this.CadenaConexion == String.Empty)
    //        {
    //            throw new Exception("Falta establecer la cadena de conexión");
    //        }

    //        if (this.ConsultaSQL == String.Empty)
    //        {
    //            throw new Exception("Falta establecer la cadena de consulta");
    //        }

    //        oSqlCommand.Connection = oSqlConnection;
    //        oSqlCommand.CommandType = Tipoconsulta;
    //        oSqlCommand.CommandText = this.ConsultaSQL;
    //        if (this.nTiempoEjecucionConsulta != -1)
    //        {
    //            oSqlCommand.CommandTimeout = nTiempoEjecucionConsulta;
    //        }

    //        for (int nInd = 0; nInd < this._NombreParametros.Count; nInd++)
    //        {
    //            // oSqlCommand.Parameters.Add("@" + Convert.ToString(_NombreParametros(nInd)), Convert.ToString(_ValorParametros(nInd)));
    //            oSqlCommand.Parameters.AddWithValue("@" + Convert.ToString(_NombreParametros[nInd]), Convert.ToString(_ValorParametros[nInd]));
    //        }

    //        // si no tiene  transaccion, asignamos la cadena de conexion y la abrimos
    //        if (oTransaccion == null)
    //        {
    //            oSqlConnection.ConnectionString = this.CadenaConexion;
    //            oSqlConnection.Open();
    //            oTransaccion = oSqlConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
    //            oSqlCommand.Transaction = ((System.Data.SqlClient.SqlTransaction)oTransaccion);
    //        }
    //        else
    //        {
    //            oSqlCommand.Transaction = ((System.Data.SqlClient.SqlTransaction)oTransaccion);
    //        }

    //        oSqlDataAdapter.SelectCommand = oSqlCommand;
    //        if ((NombreTablaDestino != null && NombreTablaDestino != String.Empty))
    //        {
    //            if (NombreTablaDestino.IndexOf(";") > -1)
    //            { //si hay mas de una tabla, hay que hacer un mapeo de ellas
    //                string[] tablas;
    //                tablas = Split(NombreTablaDestino, ";");
    //                int i = 0;
    //                oSqlDataAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping() { new System.Data.Common.DataTableMapping("Table", dsActual.Tables[0].TableName, new System.Data.Common.DataColumnMapping(-1) { }) });
    //                for (i = 1; i < tablas.Length; i++)
    //                {
    //                    oSqlCommand.Parameters.AddWithValue("@" + Convert.ToString(_NombreParametros[nInd]), Convert.ToString(_ValorParametros[nInd]));
    //                }
    //                oSqlDataAdapter.Fill(dsActual);
    //            }
    //            else
    //            {
    //                oSqlDataAdapter.Fill(dsActual, NombreTablaDestino);
    //            }
    //        }
    //        else
    //        {
    //            oSqlDataAdapter.Fill(dsActual);
    //        }
    //        if (bTiparDataset)
    //        {
    //            oSqlDataAdapter.FillSchema(dsActual, SchemaType.Mapped);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (bTransaccionCreadaEnLaFuncion)
    //        {
    //            oTransaccion.Rollback();
    //        }
    //    }
    //    finally
    //    {
    //        // si la transaccion es nula, se cierra la conexion
    //        if (oTransaccion == null || bTransaccionCreadaEnLaFuncion)
    //        {
    //            if (oSqlConnection != null)
    //            {
    //                if (oTransaccion != null && oTransaccion.Connection !== null)
    //                {
    //                    oTransaccion.Commit();
    //                    oTransaccion.Dispose();
    //                }
    //                if (oSqlConnection.State != ConnectionState.Closed)
    //                {
    //                    oSqlConnection.Close();
    //                }
    //                oSqlConnection.Dispose();
    //            }
    //        }
    //    }
    //}


    #endregion

    // TODO: saber donde se utiliza!
    //public System.Data.DataSet EjecucionConsulta(bool bTiparDataset, int filaOrigen, int nLineas, string nombreTabla)
    public System.Data.DataSet EjecucionConsulta(bool bTiparDataset, string nombreTabla)
    {
        System.Data.SqlClient.SqlConnection oSqlConnection = new System.Data.SqlClient.SqlConnection();
        System.Data.SqlClient.SqlDataAdapter oSqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter();
        System.Data.SqlClient.SqlCommand oSqlCommand = new System.Data.SqlClient.SqlCommand();
        System.Data.DataSet oDataset = new System.Data.DataSet();
        int nInd; //Contador para bucle insercion parametros
        try
        {
            //Si de antemano, sabemos que no es posible la ejecución lanzamos el error.
            if (this.CadenaConexion == String.Empty)
            {
                throw new Exception("Falta establecer la cadena de conexión");
            }

            if (this.ConsultaSQL == String.Empty)
            {
                throw new Exception("Falta establecer la cadena de consulta");
            }

            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandType = CommandType.Text;
            oSqlCommand.CommandText = this.ConsultaSQL;
            if (this.nTiempoEjecucionConsulta != -1)
            {
                oSqlCommand.CommandTimeout = nTiempoEjecucionConsulta;
            }

            for (nInd = 0; nInd < this._NombreParametros.Count; nInd++)
            {
                // oSqlCommand.Parameters.Add("@" + Convert.ToString(_NombreParametros(nInd)), Convert.ToString(_ValorParametros(nInd)));
                oSqlCommand.Parameters.AddWithValue("@" + Convert.ToString(_NombreParametros[nInd]), Convert.ToString(_ValorParametros[nInd]));

            }

            oSqlConnection.ConnectionString = this.CadenaConexion;
            oSqlConnection.Open();

            oSqlDataAdapter.SelectCommand = oSqlCommand;
            //oSqlDataAdapter.Fill(oDataset, filaOrigen, nLineas, nombreTabla);
            oSqlDataAdapter.Fill(oDataset,  nombreTabla);
            if (bTiparDataset)
            {
                oSqlDataAdapter.FillSchema(oDataset, SchemaType.Mapped);
            }

            return (oDataset);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (oSqlConnection != null)
            {
                if (oSqlConnection.State != ConnectionState.Closed)
                {
                    oSqlConnection.Close();
                }
                oSqlConnection.Dispose();
            }
        }
    }


    #region " Ejecución Actualización "
    //-----------------------------------------------------------------------------
    //Funcion: EjecucionActualizacion
    //Retorno: (System.Int32)
    //TODO : Realiza actualizaciones de tipo INSERT, UPDATE, DELETE.
    //       Retorna el numero de filas afectadas por la actualizacion (-1 si no ha afectado a ninguna fila)

    //-----------------------------------------------------------------------------
    public int EjecucionActualizacion()
    {

       
        System.Data.SqlClient.SqlConnection oSqlConnection = new System.Data.SqlClient.SqlConnection();
        System.Data.SqlClient.SqlCommand oSqlCommand = new System.Data.SqlClient.SqlCommand();
        int nInd; //Contador para bucle insercion parametros
        int nFilasAfectadas = -1;
        try
        {
            //Si de antemano, sabemos que no es posible la ejecución lanzamos el error.
            if (this.CadenaConexion == String.Empty)
            {
                throw new Exception("Falta establecer la cadena de conexión");
            }

            if (this.ConsultaSQL == String.Empty)
            {
                throw new Exception("Falta establecer la cadena de consulta");
            }

            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandType = CommandType.Text;
           // oSqlCommand.CommandType = CommandType.StoredProcedure;

            oSqlCommand.CommandText = this.ConsultaSQL;
            if (this.nTiempoEjecucionConsulta != -1)
            {
                oSqlCommand.CommandTimeout = nTiempoEjecucionConsulta;
            }

            for (nInd = 0; nInd < this._NombreParametros.Count; nInd++)
            {
                // oSqlCommand.Parameters.Add("@" + Convert.ToString(_NombreParametros(nInd)), Convert.ToString(_ValorParametros(nInd)));
                oSqlCommand.Parameters.AddWithValue("@" + Convert.ToString(_NombreParametros[nInd]), Convert.ToString(_ValorParametros[nInd]));
            }

            oSqlConnection.ConnectionString = this.CadenaConexion;
            oSqlConnection.Open();

             nFilasAfectadas = oSqlCommand.ExecuteNonQuery();
            //oSqlCommand.ExecuteNonQuery();
            //nFilasAfectadas = (int)oSqlCommand.ExecuteScalar();



        }
        finally
        {
            if (oSqlConnection != null)
            {
                if (oSqlConnection.State != ConnectionState.Closed)
                {
                    oSqlConnection.Close();
                }
                oSqlConnection.Dispose();
            }
        }
        return nFilasAfectadas;
    }



    public int Ejecucion_Insert_getId()
    {


        System.Data.SqlClient.SqlConnection oSqlConnection = new System.Data.SqlClient.SqlConnection();
        System.Data.SqlClient.SqlCommand oSqlCommand = new System.Data.SqlClient.SqlCommand();
        int nInd; //Contador para bucle insercion parametros
        int nFilasAfectadas = -1;
        try
        {
            //Si de antemano, sabemos que no es posible la ejecución lanzamos el error.
            if (this.CadenaConexion == String.Empty)
            {
                throw new Exception("Falta establecer la cadena de conexión");
            }

            if (this.ConsultaSQL == String.Empty)
            {
                throw new Exception("Falta establecer la cadena de consulta");
            }

            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandType = CommandType.Text;

            oSqlCommand.CommandText = this.ConsultaSQL + ";SELECT SCOPE_IDENTITY()";
            if (this.nTiempoEjecucionConsulta != -1)
            {
                oSqlCommand.CommandTimeout = nTiempoEjecucionConsulta;
            }

            for (nInd = 0; nInd < this._NombreParametros.Count; nInd++)
            {
                // oSqlCommand.Parameters.Add("@" + Convert.ToString(_NombreParametros(nInd)), Convert.ToString(_ValorParametros(nInd)));
                oSqlCommand.Parameters.AddWithValue("@" + Convert.ToString(_NombreParametros[nInd]), Convert.ToString(_ValorParametros[nInd]));
            }

            oSqlConnection.ConnectionString = this.CadenaConexion;
            oSqlConnection.Open();

            string getValue = oSqlCommand.ExecuteScalar().ToString();
            if (getValue != null)
            {
                nFilasAfectadas = Convert.ToInt32(getValue.ToString());
            }


            //nFilasAfectadas = oSqlCommand.ExecuteNonQuery();
            //oSqlCommand.ExecuteNonQuery();
            //nFilasAfectadas = (int)oSqlCommand.ExecuteScalar();



        }
        finally
        {
            if (oSqlConnection != null)
            {
                if (oSqlConnection.State != ConnectionState.Closed)
                {
                    oSqlConnection.Close();
                }
                oSqlConnection.Dispose();
            }
        }
        return nFilasAfectadas;
    }





    /// <summary>
    /// /
    /// </summary>
    /// <returns></returns>
    public int EjecucionProcedure()
    {
      

        System.Data.SqlClient.SqlConnection oSqlConnection = new System.Data.SqlClient.SqlConnection();
        System.Data.SqlClient.SqlCommand oSqlCommand = new System.Data.SqlClient.SqlCommand();
        int nInd; //Contador para bucle insercion parametros
        int nFilasAfectadas = -1;
        try
        {
            //Si de antemano, sabemos que no es posible la ejecución lanzamos el error.
            if (this.CadenaConexion == String.Empty)
            {
                throw new Exception("Falta establecer la cadena de conexión");
            }

            if (this.ConsultaSQL == String.Empty)
            {
                throw new Exception("Falta establecer la cadena de consulta");
            }

            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandType = CommandType.StoredProcedure;
   
   
            oSqlCommand.CommandText = this.ConsultaSQL;
            if (this.nTiempoEjecucionConsulta != -1)
            {
                oSqlCommand.CommandTimeout = nTiempoEjecucionConsulta;
            }

            for (nInd = 0; nInd < this._NombreParametros.Count; nInd++)
            {
                // oSqlCommand.Parameters.Add("@" + Convert.ToString(_NombreParametros(nInd)), Convert.ToString(_ValorParametros(nInd)));
                oSqlCommand.Parameters.AddWithValue("@" + Convert.ToString(_NombreParametros[nInd]), Convert.ToString(_ValorParametros[nInd]));
            }

            oSqlConnection.ConnectionString = this.CadenaConexion;
            oSqlConnection.Open();

            nFilasAfectadas = oSqlCommand.ExecuteNonQuery();
            //oSqlCommand.ExecuteNonQuery();
            //nFilasAfectadas = (int)oSqlCommand.ExecuteScalar();



        }
        finally
        {
            if (oSqlConnection != null)
            {
                if (oSqlConnection.State != ConnectionState.Closed)
                {
                    oSqlConnection.Close();
                }
                oSqlConnection.Dispose();
            }
        }
        return nFilasAfectadas;
    }


    #endregion

    #region " Ejecución Procedimiento Alamacenado "
    //-----------------------------------------------------------------------------
    //Funcion: EjecucionProcedimientoAlmacenado
    //Parámetro: nombreProcedimiento (System.String)
    //Parámetro: bTiparDataset (System.Boolean)
    //Parámetro: nombreTabla (System.String)
    //Retorno: (System.Data.DataSet)
    //ejecuta un store procedure de sql server pasando por parámetro un NUEVO dataset

    //-----------------------------------------------------------------------------
    public System.Data.DataSet EjecucionProcedimientoAlmacenado(string nombreProcedimiento, bool bTiparDataset, string nombreTabla)
    {
        DataSet dsActual = new DataSet();
        _EjecucionProcedimientoAlmacenado(ref dsActual, nombreProcedimiento, bTiparDataset, nombreTabla);
        return dsActual;
    }

    //-----------------------------------------------------------------------------
    //Funcion: EjecucionProcedimientoAlmacenado
    //Parámetro: dsActual (System.String)
    //Parámetro: nombreProcedimiento (System.String)
    //Parámetro: bTiparDataset (System.Boolean)
    //Parámetro: nombreTabla (System.String)
    //Retorno: (System.Data.DataSet)
    //ejecuta un store procedure de sql server pasando por parámetro un dataset concreto
    //Fecha : 20/06/2007 - 	'Usuario: 
    //-----------------------------------------------------------------------------
    public void EjecucionProcedimientoAlmacenado(ref DataSet dsActual, string nombreProcedimiento, bool bTiparDataset, string nombreTabla)
    {
        _EjecucionProcedimientoAlmacenado(ref dsActual, nombreProcedimiento, bTiparDataset, nombreTabla);
    }

    //-----------------------------------------------------------------------------
    //Funcion: EjecProcedimientoAlmacenado
    //Parámetro: dsActual (System.String)
    //Parámetro: nombreProcedimiento (System.String)
    //Parámetro: bTiparDataset (System.Boolean)
    //Parámetro: nombreTabla (System.String)
    //Retorno: (System.Data.DataSet)
    //ejecuta un store procedure de sql server
    //Fecha : 20/06/2007 - 	'Usuario:
    //-----------------------------------------------------------------------------
    private void _EjecucionProcedimientoAlmacenado(ref DataSet dsActual, string nombreProcedimiento, bool bTiparDataset, string nombreTabla)
    {
        System.Data.SqlClient.SqlConnection oSqlConnection = new System.Data.SqlClient.SqlConnection();
        System.Data.SqlClient.SqlDataAdapter oSqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter();
        System.Data.SqlClient.SqlCommand oSqlCommand = new System.Data.SqlClient.SqlCommand();
        //Dim oDataset As New System.Data.DataSet
        int nInd; //Contador para bucle insercion parametros
        try
        {
            //Si de antemano, sabemos que no es posible la ejecución lanzamos el error.
            if (this.CadenaConexion == String.Empty)
            {
                throw new Exception("Falta establecer la cadena de conexión");
            }

            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandType = CommandType.StoredProcedure;
            oSqlCommand.CommandText = nombreProcedimiento;
            if (this.nTiempoEjecucionConsulta != -1)
            {
                oSqlCommand.CommandTimeout = nTiempoEjecucionConsulta;
            }

            for (nInd = 0; nInd < this._NombreParametros.Count; nInd++)
            {
                oSqlCommand.Parameters.AddWithValue("@" + Convert.ToString(_NombreParametros[nInd]), Convert.ToString(_ValorParametros[nInd]));
            }
            oSqlConnection.ConnectionString = this.CadenaConexion;
            oSqlConnection.Open();

            oSqlDataAdapter.SelectCommand = oSqlCommand;
            oSqlDataAdapter.Fill(dsActual, nombreTabla);
            if (bTiparDataset)
            {
                oSqlDataAdapter.FillSchema(dsActual, SchemaType.Mapped);
            }
        }
        finally
        {
            if (oSqlConnection != null)
            {
                if (oSqlConnection.State != ConnectionState.Closed)
                {
                    oSqlConnection.Close();
                }
                oSqlConnection.Dispose();
            }
        }
    }
    #endregion

    #region " Privadas "
    //-----------------------------------------------------------------------------
    //Funcion: EstablecerCadenaConexion
    //Parámetro: cadenaconexion (System.String)
    //Establece la cadena de conexion por parametro o la obtiene del fichero de configuración de la aplicación

    //-----------------------------------------------------------------------------
    private void EstablecerCadenaConexion(string cadenaconexion)
    {
        if (cadenaconexion != String.Empty)
        {
            this.CadenaConexion = cadenaconexion;
        }
        else
        {
  
            this.CadenaConexion = @"Data Source=86.109.106.101;Database=Tecdoc;User Id=SercaTarifas;Password=SerTar08;";
        

        }
    }

    //-----------------------------------------------------------------------------
    //Funcion: EstablecerConsultaSQL
    //Parámetro: consultaSQL (System.String)
    //Establecimiento de la cadena de consulta SQL
    //Fecha : 22/07/2004 - 	
    //-----------------------------------------------------------------------------
    private void EstablecerConsultaSQL(string consultaSQL)
    {
        if (consultaSQL != String.Empty)
        {
            this.ConsultaSQL = consultaSQL;
        }
    }
    #endregion

    //uso

    //String tcSQL = "SELECT * FROM SEO where SEO_PAGE_ID=@Id ";
    //String tcSQL2 = "INSERT into SEO  (NOMBRE, PAGE ,ACTIVE,SITEMAP ) values ( @Nombre, 'SitemapWeb.aspx', 1  , 1)";

    //AccesoDatosSQLCliente accesoDatosSQLCliente2 = new AccesoDatosSQLCliente("", tcSQL2);
    //accesoDatosSQLCliente2.AñadirParametro("Nombre", "SiteMap1");
    //    accesoDatosSQLCliente2.EjecucionActualizacion();

    //    AccesoDatosSQLCliente accesoDatosSQLCliente = new AccesoDatosSQLCliente("", tcSQL);
    //accesoDatosSQLCliente.AñadirParametro("Id", "1");
    //    // accesoDatosSQLCliente.AñadirParametro("B", "B");

    //    DataSet ds = accesoDatosSQLCliente.EjecucionConsulta(false, "tablaSeo");
    //accesoDatosSQLCliente.EjecucionActualizacion();



}