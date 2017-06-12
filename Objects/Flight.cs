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

    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }
    public DateTime GetDepart()
    {
      return _depart;
    }
    public void SetDepart(DateTime newDateTime)
    {
      _depart = newDateTime;
    }
    public string GetStatus()
    {
      return _status;
    }
    public void SetStatus(string newStatus)
    {
      _status = newStatus;
    }

    public int GetId()
    {
      return _id;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO flights (name, depart, status) OUTPUT INSERTED.id VALUES (@FlightName, @FlightDepart, @FlightStatus);", conn);
      SqlParameter nameParam = new SqlParameter("@FlightName", this.GetName());
      Console.WriteLine(this.GetName());
      SqlParameter departParam = new SqlParameter("@FlightDepart", this.GetDepart());
      SqlParameter statusParam = new SqlParameter("@FlightStatus", this.GetStatus());

      cmd.Parameters.Add(nameParam);
      cmd.Parameters.Add(departParam);
      cmd.Parameters.Add(statusParam);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
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
        bool nameEquality = (this.GetName() == newFlight.GetName());
        bool departEquality = (this.GetDepart() == newFlight.GetDepart());
        bool statusEquality = (this.GetStatus() == newFlight.GetStatus());

        return (idEquality && nameEquality);
      }
    }
    // public override int GetHashCode()
    // {
    //   return this.Name.GetHashCode();
    // }

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
