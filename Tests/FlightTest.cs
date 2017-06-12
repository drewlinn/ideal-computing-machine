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

    [Fact]
    public void Test_Equal_ReturnsSame()
    {
      //arrange, act
      Flight flightOne = new Flight("Alaska", new DateTime(2016, 1, 12, 23, 59, 59), "On-Time");
      Flight flightTwo = new Flight("Alaska", new DateTime(2016, 1, 12, 23, 59, 59), "On-Time");
      //assert
      Assert.Equal(flightOne, flightTwo);
    }

    [Fact]
    public void Test_Save_SavesToDB()
    {

    }
    
    public void Dispose()
    {
      Flight.DeleteAll();
    }
  }
}
