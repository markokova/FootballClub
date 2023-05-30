using FootballClub.Staff.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using System.Net;

namespace FootballClub.Staff.Database_Logic
{
    public class ReservationDBHandler
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        public int InsertReservation(Reservation Reservation)
        {
            Guid id = Guid.NewGuid();
            Reservation.Id = id;
            int affectedRows = 0;
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO \"Reservation\" (\"Id\", \"ReservationDate\", \"CarId\", \"PersonId\") VALUES (@Value1, @Value2, @Value3, @Value4)";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Value1", Reservation.Id);
                        command.Parameters.AddWithValue("@Value2", Reservation.ReservationDate);
                        command.Parameters.AddWithValue("@Value3", Reservation.CarId);
                        command.Parameters.AddWithValue("@Value4", Reservation.PersonId);
                        affectedRows = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
            }
            return affectedRows;
        }

        public List<Dictionary<string,string>> GetReservations()
        {
            Reservation reservation = null; Car car = null; Person person = null;
            List<Dictionary<string,string>> reservations = new List<Dictionary<string,string>>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    //string query = "SELECT * FROM \"Reservation\"";
                    string query = "SELECT r.\"Id\" AS ReservationId, r.\"ReservationDate\", c.\"Id\" AS CarId, c.\"Manufacturer\", c.\"Model\", c.\"NumberOfSeats\", c.\"Price\", p.\"FirstName\", p.\"LastName\", p.\"Email\" " +
                        "FROM \"Reservation\" r " +
                        "INNER JOIN \"Car\" c ON r.\"CarId\" = c.\"Id\" " +
                        "INNER JOIN \"Person\" p ON r.\"PersonId\" = p.\"Id\"";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        NpgsqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                reservation = new Reservation(); car = new Car(); person = new Person();
                                reservation.Id = (Guid)reader["ReservationId"];
                                reservation.ReservationDate = (DateTime)reader["ReservationDate"];
                                reservation.CarId = (Guid)reader["CarId"];
                                car.Manufacturer = (string)reader["Manufacturer"];
                                car.Model = (string)reader["Model"];
                                car.NumberOfSeats = (int)reader["NumberOfSeats"];
                                car.Price = (double)reader["Price"];
                                person.FirstName = (string)reader["FirstName"];
                                person.LastName = (string)reader["LastName"];
                                person.Email = (string)reader["Email"];
                                Dictionary<string, string> dictionary = ToDictionary((reservation, car, person));
                                if(dictionary != null)
                                {
                                    reservations.Add(dictionary);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
            }
            return reservations;
        }

        //TODO - Is it better to return Dictionary<string,string> as I return here (although there are integers and doubles in car data) or to return
        //a Tuple (Reservation, Car, Person) response and then in the controller get method return this:
        //return Request.CreateResponse(HttpStatusCode.OK, new {Reservation = response.reservation, Car = response.car, Person = response.person});
        //or there is another way to fetch attributes of multilpe objects and return them

        public Dictionary<string, string> GetReservation(Guid id)
        {
            Reservation reservation = null; Car car = null; Person person = null;
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT r.\"Id\" AS ReservationId, r.\"ReservationDate\", c.\"Id\" AS CarId, c.\"Manufacturer\",c.\"Model\",c.\"NumberOfSeats\", c.\"Price\", p.\"FirstName\", p.\"LastName\", p.\"Email\" " +
                        "FROM \"Reservation\" r " +
                        "INNER JOIN \"Car\" c ON r.\"CarId\" = c.\"Id\" " +
                        "INNER JOIN \"Person\" p ON r.\"PersonId\" = p.\"Id\" " +
                        "WHERE r.\"Id\" =@ReservationId";
                    //string query = "SELECT * FROM \"Reservation\" WHERE \"Id\" = @ReservationId";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ReservationId", id);
                        NpgsqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reservation = new Reservation(); car = new Car(); person = new Person();
                            reader.Read();
                            reservation.Id = (Guid)reader["ReservationId"];
                            reservation.ReservationDate = (DateTime)reader["ReservationDate"];
                            car.Manufacturer = (string)reader["Manufacturer"];
                            car.Model = (string)reader["Model"];
                            car.NumberOfSeats = (int)reader["NumberOfSeats"];
                            car.Price = (double)reader["Price"];
                            person.FirstName = (string)reader["FirstName"];
                            person.LastName = (string)reader["LastName"];
                            person.Email = (string)reader["Email"];
                            reservation.CarId = (Guid)reader["CarId"];
                            reservation.PersonId = (Guid)reader["PersonId"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
            }
            Dictionary<string, string> response = ToDictionary((reservation, car, person));
            return response;
        }

        public int UpdateReservation(Guid id, Reservation newReservation)
        {
            Reservation oldReservation = this.GetReservationById(id);
            int affectedRows = 0;
            StringBuilder builder = new StringBuilder("UPDATE \"Reservation\" SET ");

            try
            {
                if (oldReservation != null)
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {
                            if (newReservation.ReservationDate != DateTime.MinValue)
                            {
                                builder.Append("\"ReservationDate\" = @ReservationDateValue,");
                                command.Parameters.AddWithValue("@ReservationDateValue", newReservation.ReservationDate);

                            }
                            if (newReservation.CarId  != Guid.Empty)
                            {
                                builder.Append("\"CarId\" = @CarIdValue,");
                                command.Parameters.AddWithValue("@CarIdValue", newReservation.CarId);

                            }
                            if (newReservation.PersonId != Guid.Empty)
                            {
                                builder.Append("\"PersonId\" = @PersonIdValue,");
                                command.Parameters.AddWithValue("@PersonIdValue", newReservation.PersonId);
                            }
                            if (builder.ToString().EndsWith(","))
                            {
                                if (builder.Length > 0)
                                {
                                    builder.Remove(builder.Length - 1, 1);
                                }
                            }
                            builder.Append(" WHERE \"Id\" = @OldIdValue");
                            string query = builder.ToString();
                            command.Parameters.AddWithValue("@OldIdValue", oldReservation.Id);
                            command.CommandText = query;
                            command.Connection = connection;
                            affectedRows = command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
            }
            return affectedRows;
        }

        public int DeleteReservation(Guid id)
        {
            Reservation ReservationToDelete = this.GetReservationById(id);
            int affectedRows = 0;
            try
            {
                if (ReservationToDelete != null)
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM \"Reservation\" WHERE \"Id\" = @IdValue";
                        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@IdValue", id);
                            affectedRows = command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
            }
            return affectedRows;
        }


        private Reservation GetReservationById(Guid id)
        {
            Reservation reservation = null;
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM \"Reservation\" WHERE \"Id\" = @ReservationId";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ReservationId", id);
                        NpgsqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reservation = new Reservation();
                            reader.Read();
                            reservation.Id = (Guid)reader["Id"];
                            reservation.ReservationDate = (DateTime)reader["ReservationDate"];
                            reservation.CarId = (Guid)reader["CarId"];
                            reservation.PersonId = (Guid)reader["PersonId"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
            }
            return reservation;
        }

        private Dictionary<string, string> ToDictionary((Reservation reservation, Car car, Person person) responseTuple)
        {
            if (responseTuple.reservation != null)
            {
                Dictionary<string, string> response = new Dictionary<string, string>()
                {
                    {"Reservation Id", responseTuple.reservation.Id.ToString()},
                    {"Reservation Date", responseTuple.reservation.ReservationDate.ToString()},
                    {"Car Manufacturer",responseTuple.car.Manufacturer },
                    {"Car Model", responseTuple.car.Model },
                    {"Car number of seats",responseTuple.car.NumberOfSeats.ToString() },
                    {"Price [€]", responseTuple.car.Price.ToString() },
                    {"First Name", responseTuple.person.FirstName },
                    {"Last Name", responseTuple.person.LastName },
                    {"Email", responseTuple.person.Email },

                };
                return response;
            }
            return null;
        }
    }
}