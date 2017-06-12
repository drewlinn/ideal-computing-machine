using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace AirlinePlanner
{
  public class Flight
  {
    private string _name;
    private DateTime _depart;
    private string _status;
    private int _id;

    public Flight(string Name, DateTime Depart, string Status, int Id = 0)
    {
      _name = Name;
      _depart = Depart;
      _status = Status;
      _id = Id;
    }

    public string Name {get; set;}
    public DateTime Depart {get; set;}
    public string Status {get; set;}

    public int GetId()
    {
      return _id;
    }

    public static List<Flight> GetAll()
    {
      List<Flight> allFlights = new List<Flight> {};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM flights;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int flightId = rdr.GetInt32(0);
        string flightName = rdr.GetString(1);
        DateTime flightDepart = rdr.GetDateTime(2);
        string flightStatus = rdr.GetString(3);
        Flight newFlight = new Flight(flightName, flightDepart, flightStatus, flightId);
        allFlights.Add(newFlight);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allFlights;
    }

    public override bool Equals(System.Object otherFlight)
    {
      if (!(otherFlight is Flight))
      {
        return false;
      }
      else
      {
        Flight newFlight = (Flight) otherFlight;
        bool idEquality = (this.GetId() == newFlight.GetId());
        bool nameEquality = (this.Name == newFlight.Name);
        bool departEquality = (this.Depart == newFlight.Depart);
        bool statusEquality = (this.Status == newFlight.Status);

        return (idEquality && nameEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.Name.GetHashCode();
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM flights;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
     }
  }
}
