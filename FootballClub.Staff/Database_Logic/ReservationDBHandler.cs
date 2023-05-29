using FootballClub.Staff.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;

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

        public List<Reservation> GetReservations()
        {
            List<Reservation> Reservations = new List<Reservation>();
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM \"Reservation\"";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        NpgsqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Reservation Reservation = new Reservation();
                                Reservation.Id = (Guid)reader["Id"];
                                Reservation.ReservationDate = (DateTime)reader["ReservationDate"];
                                Reservation.CarId = (Guid)reader["CarId"];
                                Reservation.PersonId = (Guid)reader["PersonId"];
                                Reservations.Add(Reservation);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
            }
            return Reservations;
        }

        public Reservation GetReservation(Guid id)
        {
            return this.GetReservationById(id);
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
            Reservation Reservation = null;
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM \"Reservation\" WHERE \"Id\" = @Value";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Value", id);
                        NpgsqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            Reservation = new Reservation();
                            reader.Read();
                            Reservation.Id = (Guid)reader["Id"];
                            Reservation.ReservationDate = (DateTime)reader["ReservationDate"];
                            Reservation.CarId = (Guid)reader["CarId"];
                            Reservation.PersonId = (Guid)reader["PersonId"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
            }
            return Reservation;
        }
    }
}