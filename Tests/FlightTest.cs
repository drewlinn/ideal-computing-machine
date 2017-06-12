using Xunit;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace AirlinePlanner
{
  [Collection("AirlinePlanner")]
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
      Flight flightOne = new Flight("Alaska", new DateTime(2016, 01, 12, 23, 00, 00), "On-Time");
      Flight flightTwo = new Flight("Alaska", new DateTime(2016, 01, 12, 23, 00, 00), "On-Time");
      //assert
      Assert.Equal(flightOne, flightTwo);
    }

    [Fact]
    public void Test_Save_SavesToDB()
    {
      //arrange, act
      Flight newFlight = new Flight("Alaska", DateTime.Now, "On-Time");
      newFlight.Save();

      List<Flight> result = Flight.GetAll();
      List<Flight> allFlights  = new List<Flight> {newFlight};
      //assert
      Assert.Equal(result, allFlights);
    }

    public void Dispose()
    {
      Flight.DeleteAll();
    }
  }
}
