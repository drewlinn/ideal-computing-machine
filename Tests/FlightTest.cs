using Xunit;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace AirlinePlanner
{
  public class FlightTest : IDisposable
  {
    public FlightTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=airline_planner_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DBEmpty()
    {
      //arrange, act
      int result = Flight.GetAll().Count;
      //assert
      Assert.Equal(0, result);
    }

    public void Dispose()
    {
      Flight.DeleteAll();
    }
  }
}
