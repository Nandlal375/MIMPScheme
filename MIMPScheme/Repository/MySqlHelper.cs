

using System.Data;
//using MySql.Data.MySqlClient;
using System.Text;
using MySqlConnector;

namespace MIMPScheme.Data.Helper
{
  /// <summary>
  /// Helper class that makes it easier to work with the provider.
  /// </summary>
  public class MySqlHelperClass
  {
    //enum CharClass : byte
    //{
    //  None,
    //  Quote,
    //  Backslash
    //}

    //private static string stringOfBackslashChars = "\u005c\u00a5\u0160\u20a9\u2216\ufe68\uff3c";
    //private static string stringOfQuoteChars =
    //    "\u0022\u0027\u0060\u00b4\u02b9\u02ba\u02bb\u02bc\u02c8\u02ca\u02cb\u02d9\u0300\u0301\u2018\u2019\u201a\u2032\u2035\u275b\u275c\uff07";

    //private static CharClass[] charClassArray = makeCharClassArray();

    // this class provides only static methods
    //private MySqlHelper()
    //{
    //}

    #region ExecuteNonQuery

    /// <summary>
    /// Executes a single command against a MySQL database.  The <see cref="MySqlConnection"/> is assumed to be
    /// open when the method is called and remains open after the method completes.
    /// </summary>
    /// <param name="connection"><see cref="MySqlConnection"/> object to use</param>
    /// <param name="commandText">SQL command to be executed</param>
    /// <param name="commandParameters">Array of <see cref="MySqlParameter"/> objects to use with the command.</param>
    /// <returns></returns>
    public static int ExecuteNonQuery(MySqlConnection connection, string commandText, params MySqlParameter[] commandParameters)
    {
      //create a command and prepare it for execution
      MySqlCommand cmd = new MySqlCommand();
      cmd.Connection = connection;
      cmd.CommandText = commandText;
      cmd.CommandType = CommandType.StoredProcedure;

      if (commandParameters != null)
        foreach (MySqlParameter p in commandParameters)
          cmd.Parameters.Add(p);

      int result = cmd.ExecuteNonQuery();
      cmd.Parameters.Clear();

      return result;
    }

    /// <summary>
    /// Executes a single command against a MySQL database.  A new <see cref="MySqlConnection"/> is created
    /// using the <see cref="MySqlConnection.ConnectionString"/> given.
    /// </summary>
    /// <param name="connectionString"><see cref="MySqlConnection.ConnectionString"/> to use</param>
    /// <param name="commandText">SQL command to be executed</param>
    /// <param name="parms">Array of <see cref="MySqlParameter"/> objects to use with the command.</param>
    /// <returns></returns>
    public static int ExecuteNonQuery(string connectionString, string commandText, params MySqlParameter[] parms)
    {
      //create & open a SqlConnection, and dispose of it after we are done.
      using (MySqlConnection cn = new MySqlConnection(connectionString))
      {
        cn.Open();

        //call the overload that takes a connection in place of the connection string
        return ExecuteNonQuery(cn, commandText, parms);
      }
    }
    #endregion

    #region ExecuteDataSet

    /// <summary>
    /// Executes a single SQL command and returns the first row of the resultset.  A new MySqlConnection object
    /// is created, opened, and closed during this method.
    /// </summary>
    /// <param name="connectionString">Settings to be used for the connection</param>
    /// <param name="commandText">Command to execute</param>
    /// <param name="parms">Parameters to use for the command</param>
    /// <returns>DataRow containing the first row of the resultset</returns>
    public static DataRow ExecuteDataRow(string connectionString, string commandText, params MySqlParameter[] parms)
    {
      DataSet ds = ExecuteDataset(connectionString, commandText, parms);
      if (ds == null) return null;
      if (ds.Tables.Count == 0) return null;
      if (ds.Tables[0].Rows.Count == 0) return null;
      return ds.Tables[0].Rows[0];
    }

    /// <summary>
    /// Executes a single SQL command and returns the resultset in a <see cref="DataSet"/>.  
    /// A new MySqlConnection object is created, opened, and closed during this method.
    /// </summary>
    /// <param name="connectionString">Settings to be used for the connection</param>
    /// <param name="commandText">Command to execute</param>
    /// <returns><see cref="DataSet"/> containing the resultset</returns>
    public static DataSet ExecuteDataset(string connectionString, string commandText)
    {
      //pass through the call providing null for the set of SqlParameters
      return ExecuteDataset(connectionString, commandText, (MySqlParameter[])null);
    }

    /// <summary>
    /// Executes a single SQL command and returns the resultset in a <see cref="DataSet"/>.  
    /// A new MySqlConnection object is created, opened, and closed during this method.
    /// </summary>
    /// <param name="connectionString">Settings to be used for the connection</param>
    /// <param name="commandText">Command to execute</param>
    /// <param name="commandParameters">Parameters to use for the command</param>
    /// <returns><see cref="DataSet"/> containing the resultset</returns>
    public static DataSet ExecuteDataset(string connectionString, string commandText, params MySqlParameter[] commandParameters)
    {
      //create & open a SqlConnection, and dispose of it after we are done.
      using (MySqlConnection cn = new MySqlConnection(connectionString))
      {
        cn.Open();

        //call the overload that takes a connection in place of the connection string
        return ExecuteDataset(cn, commandText, commandParameters);
      }
    }

    /// <summary>
    /// Executes a single SQL command and returns the resultset in a <see cref="DataSet"/>.  
    /// The state of the <see cref="MySqlConnection"/> object remains unchanged after execution
    /// of this method.
    /// </summary>
    /// <param name="connection"><see cref="MySqlConnection"/> object to use</param>
    /// <param name="commandText">Command to execute</param>
    /// <returns><see cref="DataSet"/> containing the resultset</returns>
    public static DataSet ExecuteDataset(MySqlConnection connection, string commandText)
    {
      //pass through the call providing null for the set of SqlParameters
      return ExecuteDataset(connection, commandText, (MySqlParameter[])null);
    }

    /// <summary>
    /// Executes a single SQL command and returns the resultset in a <see cref="DataSet"/>.  
    /// The state of the <see cref="MySqlConnection"/> object remains unchanged after execution
    /// of this method.
    /// </summary>
    /// <param name="connection"><see cref="MySqlConnection"/> object to use</param>
    /// <param name="commandText">Command to execute</param>
    /// <param name="commandParameters">Parameters to use for the command</param>
    /// <returns><see cref="DataSet"/> containing the resultset</returns>
    public static DataSet ExecuteDataset(MySqlConnection connection, string commandText, params MySqlParameter[] commandParameters)
    {
      //create a command and prepare it for execution
      MySqlCommand cmd = new MySqlCommand();
      cmd.Connection = connection;
      cmd.CommandText = commandText;
      cmd.CommandType = CommandType.Text;

      if (commandParameters != null)
        foreach (MySqlParameter p in commandParameters)
          cmd.Parameters.Add(p);

      //create the DataAdapter & DataSet
      MySqlDataAdapter da = new MySqlDataAdapter(cmd);
      DataSet ds = new DataSet();

      //fill the DataSet using default values for DataTable names, etc.
      da.Fill(ds);

      // detach the MySqlParameters from the command object, so they can be used again.			
      cmd.Parameters.Clear();

      //return the dataset
      return ds;
    }

    /// <summary>
    /// Updates the given table with data from the given <see cref="DataSet"/>
    /// </summary>
    /// <param name="connectionString">Settings to use for the update</param>
    /// <param name="commandText">Command text to use for the update</param>
    /// <param name="ds"><see cref="DataSet"/> containing the new data to use in the update</param>
    /// <param name="tablename">Tablename in the dataset to update</param>
    public static void UpdateDataSet(string connectionString, string commandText, DataSet ds, string tablename)
    {
      MySqlConnection cn = new MySqlConnection(connectionString);
      cn.Open();
      MySqlDataAdapter da = new MySqlDataAdapter(commandText, cn);
      MySqlCommandBuilder cb = new MySqlCommandBuilder(da);
      cb.ToString();
      da.Update(ds, tablename);
      cn.Close();
    }

        #endregion

        #region ExecuteDataReader

        private static MySqlDataReader ExecuteReader(MySqlConnection connection, MySqlTransaction transaction, string commandText, MySqlParameter[] commandParameters, bool ExternalConn)
        {
            //create a command and prepare it for execution
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.Transaction = transaction;
            cmd.CommandText = commandText;
            cmd.CommandType = CommandType.StoredProcedure;

            if (commandParameters != null)
                foreach (MySqlParameter p in commandParameters)
                    cmd.Parameters.Add(p);

            //create a reader
            MySqlDataReader dr;

            // call ExecuteReader with the appropriate CommandBehavior
            if (ExternalConn)
            {
                dr = cmd.ExecuteReader();
            }
            else
            {
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }

            // detach the SqlParameters from the command object, so they can be used again.
            cmd.Parameters.Clear();

            return dr;
        }
        //private static MySqlDataReader ExecuteReader(MySqlConnection connection,string commandText, MySqlParameter[] commandParameters)
        //{
        //    //create a command and prepare it for execution
        //    MySqlCommand cmd = new MySqlCommand();
        //    //MySqlConnection cn = new MySqlConnection(connection);              
         
        //    cmd.CommandText = commandText;
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Connection=connection;

        //    if (commandParameters != null)
        //        foreach (MySqlParameter p in commandParameters)
        //            cmd.Parameters.Add(p);

        //    //create a reader
        //    MySqlDataReader dr;

        //    // call ExecuteReader with the appropriate CommandBehavior
           
        //        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            

        //    // detach the SqlParameters from the command object, so they can be used again.
        //    cmd.Parameters.Clear();

        //    return dr;
        //}
        
    //    public static MySqlDataReader ExecuteReader(string connectionString, string commandText)
    //{
    //  //pass through the call providing null for the set of SqlParameters
    //  return ExecuteReader(connectionString, commandText, (MySqlParameter[])null);
    //}

    
    public static MySqlDataReader ExecuteReader(MySqlConnection connection, string commandText)
    {
      //pass through the call providing null for the set of SqlParameters
      return ExecuteReader(connection, null, commandText, (MySqlParameter[])null, true);
    }

    /// <summary>
    /// Executes a single command against a MySQL database.
    /// </summary>
    /// <param name="connectionString">Settings to use for this command</param>
    /// <param name="commandText">Command text to use</param>
    /// <param name="commandParameters">Array of <see cref="MySqlParameter"/> objects to use with the command</param>
    /// <returns><see cref="MySqlDataReader"/> object ready to read the results of the command</returns>
    public static MySqlDataReader ExecuteReader(MySqlConnection cn, string commandText, params MySqlParameter[] commandParameters)
    {
      //create & open a SqlConnection
     
     

            //call the private overload that takes an internally owned connection in place of the connection string
            MySqlCommand cmd = new MySqlCommand();
            //MySqlConnection cn = new MySqlConnection(connection);
            cmd.CommandText = commandText;
            //cmd = new MySqlCommand("ValidateUser", cn);
            
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;

            if (commandParameters != null)
                foreach (MySqlParameter p in commandParameters)
                    cmd.Parameters.Add(p);

            //create a reader
            MySqlDataReader dr;

            // call ExecuteReader with the appropriate CommandBehavior

            //dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cn.Open();
            dr = cmd.ExecuteReader();
          //  cmd.ExecuteNonQuery();
            
           


            // detach the SqlParameters from the command object, so they can be used again.
            cmd.Parameters.Clear();

            return dr;
        }

        // public static MySqlDataReader ExecuteReader(MySqlConnection connection, string commandText, params MySqlParameter[] commandParameters)
        //{
        //  //call the private overload that takes an internally owned connection in place of the connection string
        //  return ExecuteReader(connection, null, commandText, commandParameters, true);
        //}


        #endregion

        #region ExecuteScalar

        /// <summary>
        /// Execute a single command against a MySQL database.
        /// </summary>
        /// <param name="connectionString">Settings to use for the update</param>
        /// <param name="commandText">Command text to use for the update</param>
        /// <returns>The first column of the first row in the result set, or a null reference if the result set is empty.</returns>
        public static object ExecuteScalar(string connectionString, string commandText)
    {
      //pass through the call providing null for the set of MySqlParameters
      return ExecuteScalar(connectionString, commandText, (MySqlParameter[])null);
    }

    /// <summary>
    /// Execute a single command against a MySQL database.
    /// </summary>
    /// <param name="connectionString">Settings to use for the command</param>
    /// <param name="commandText">Command text to use for the command</param>
    /// <param name="commandParameters">Parameters to use for the command</param>
    /// <returns>The first column of the first row in the result set, or a null reference if the result set is empty.</returns>
    public static object ExecuteScalar(string connectionString, string commandText, params MySqlParameter[] commandParameters)
    {
      //create & open a SqlConnection, and dispose of it after we are done.
      using (MySqlConnection cn = new MySqlConnection(connectionString))
      {
        cn.Open();

        //call the overload that takes a connection in place of the connection string
        return ExecuteScalar(cn, commandText, commandParameters);
      }
    }

    /// <summary>
    /// Execute a single command against a MySQL database.
    /// </summary>
    /// <param name="connection"><see cref="MySqlConnection"/> object to use</param>
    /// <param name="commandText">Command text to use for the command</param>
    /// <returns>The first column of the first row in the result set, or a null reference if the result set is empty.</returns>
    public static object ExecuteScalar(MySqlConnection connection, string commandText)
    {
      //pass through the call providing null for the set of MySqlParameters
      return ExecuteScalar(connection, commandText, (MySqlParameter[])null);
    }

    /// <summary>
    /// Execute a single command against a MySQL database.
    /// </summary>
    /// <param name="connection"><see cref="MySqlConnection"/> object to use</param>
    /// <param name="commandText">Command text to use for the command</param>
    /// <param name="commandParameters">Parameters to use for the command</param>
    /// <returns>The first column of the first row in the result set, or a null reference if the result set is empty.</returns>
    public static object ExecuteScalar(MySqlConnection connection, string commandText, params MySqlParameter[] commandParameters)
    {
      //create a command and prepare it for execution
      MySqlCommand cmd = new MySqlCommand();
      cmd.Connection = connection;
      cmd.CommandText = commandText;
      cmd.CommandType = CommandType.Text;

      if (commandParameters != null)
        foreach (MySqlParameter p in commandParameters)
          cmd.Parameters.Add(p);

      //execute the command & return the results
      object retval = cmd.ExecuteScalar();

      // detach the SqlParameters from the command object, so they can be used again.
      cmd.Parameters.Clear();
      return retval;

    }

    #endregion

    #region Utility methods
    //private static CharClass[] makeCharClassArray()
    //{

    //  CharClass[] a = new CharClass[65536];
    //  foreach (char c in stringOfBackslashChars)
    //  {
    //    a[c] = CharClass.Backslash;
    //  }
    //  foreach (char c in stringOfQuoteChars)
    //  {
    //    a[c] = CharClass.Quote;
    //  }
    //  return a;
    //}

    //private static bool needsQuoting(string s)
    //{
    //  foreach (char c in s)
    //  {
    //    if (charClassArray[c] != CharClass.None)
    //    {
    //      return true;
    //    }
    //  }
    //  return false;
    //}

    /// <summary>
    /// Escapes the string.
    /// </summary>
    /// <param name="value">The string to escape</param>
    /// <returns>The string with all quotes escaped.</returns>
    //public static string EscapeString(string value)
    //{
    //  if (!needsQuoting(value))
    //    return value;

    //  StringBuilder sb = new StringBuilder();

    //  foreach (char c in value)
    //  {
    //    CharClass charClass = charClassArray[c];
    //    if (charClass != CharClass.None)
    //    {
    //      sb.Append("\\");
    //    }
    //    sb.Append(c);
    //  }
    //  return sb.ToString();
    //}

    //public static string DoubleQuoteString(string value)
    //{
    //  if (!needsQuoting(value))
    //    return value;

    //  StringBuilder sb = new StringBuilder();
    //  foreach (char c in value)
    //  {
    //    CharClass charClass = charClassArray[c];
    //    if (charClass == CharClass.Quote)
    //      sb.Append(c);
    //    else if (charClass == CharClass.Backslash)
    //      sb.Append("\\");
    //    sb.Append(c);
    //  }
    //  return sb.ToString();
    //}

    #endregion
  }
}
