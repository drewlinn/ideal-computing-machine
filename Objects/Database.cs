using System;
using System.Data;
using System.Data.SqlClient;

namespace AirlinePlanner
{
  public class DB
  {
    public static SqlConnection Connection()
    {
      SqlConnection conn = new SqlConnection(DBConfiguration.ConnectionString);
      return conn;
    }
  }
}
