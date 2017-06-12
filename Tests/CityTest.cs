using Xunit;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace AirlinePlanner
{
  public class CityTest : IDisposable
  {
    public CityTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=airline_planner_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Flight.DeleteAll();
    }
  }
}
