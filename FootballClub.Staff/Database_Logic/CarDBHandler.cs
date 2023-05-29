using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FootballClub.Staff.Models;
using System.Xml.Linq;
using System.Diagnostics;
using System.Web.WebPages;
using System.Text;
using System.Configuration;

namespace FootballClub.Staff.Database_Logic
{
    public class CarDBHandler
    {
        private string connectionString = "Server=localhost;Port=5432;Database=Rent_a_car;User Id=postgres;Password=password";
        private string connString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        public void InsertCar(Car car)
        {
            Guid id = Guid.NewGuid();
            car.Id = id;
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO \"Car\" (\"Id\", \"Manufacturer\", \"Model\", \"NumberOfSeats\", \"Price\") VALUES (@Value1, @Value2, @Value3, @Value4, @Value5)";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Value1", car.Id);
                        command.Parameters.AddWithValue("@Value2", car.Manufacturer);
                        command.Parameters.AddWithValue("@Value3", car.Model);
                        command.Parameters.AddWithValue("@Value4", car.NumberOfSeats);
                        command.Parameters.AddWithValue("@Value5", car.Price);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
            }

        }

        public List<Car> GetCars()
        {
            List<Car> cars = new List<Car>();
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM \"Car\"";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        NpgsqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Car car = new Car();
                                car.Id = (Guid)reader["Id"];
                                car.Manufacturer = (string)reader["Manufacturer"];
                                car.Model = (string)reader["Model"];
                                car.NumberOfSeats = (int)reader["NumberOfSeats"];
                                car.Price = (double)reader["Price"];
                                cars.Add(car);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
            }
            return cars;
        }

        public Car GetCar(Guid id)
        {
            return this.GetCarById(id);
        }
        private Car GetCarById(Guid id)
        {
            Car car = new Car();
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM \"Car\" WHERE \"Id\" = @Value";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Value", id);
                        NpgsqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            car.Id = (Guid)reader["Id"];
                            car.Manufacturer = (string)reader["Manufacturer"];
                            car.Model = (string)reader["Model"];
                            car.NumberOfSeats = (int)reader["NumberOfSeats"];
                            car.Price = (double)reader["Price"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
            }
            return car;
        }
        
        public bool UpdateCar(Guid id, Car newCar)
        {
            Car oldCar = this.GetCarById(id);
            string manufacture = string.Empty;
            string model = string.Empty;
            int numberOfSeats = 0;
            double price = 0;

            StringBuilder builder = new StringBuilder("UPDATE \"Car\" SET ");

            try
            {
                if (oldCar != null)
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {
                            if(!newCar.Manufacturer.IsEmpty())
                            {
                                builder.Append("\"Manufacturer\" = @ManufactureValue,");
                                manufacture = newCar.Manufacturer;
                                command.Parameters.AddWithValue("@ManufactureValue", manufacture);

                            }
                            if (!newCar.Model.IsEmpty())
                            {
                                builder.Append("\"Model\" = @ModelValue,");
                                model = newCar.Model;
                                command.Parameters.AddWithValue("@ModelValue", model);

                            }
                            if (newCar.NumberOfSeats != 0)
                            {
                                builder.Append("\"NumberOfSeats\" = @NumberOfSeatsValue,");
                                numberOfSeats = newCar.NumberOfSeats;
                                command.Parameters.AddWithValue("@NumberOfSeatsValue", numberOfSeats);
                            }
                            if (newCar.Price != 0)
                            {
                                builder.Append("\"Price\" = @PriceValue,");
                                price = newCar.Price;
                                command.Parameters.AddWithValue("@PriceValue", price);
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
                            command.Parameters.AddWithValue("@OldIdValue", oldCar.Id);
                            command.CommandText = query;
                            command.Connection = connection;
                            command.ExecuteNonQuery();
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
            }
            return false;
        }

        public bool DeleteCar(Guid id)
        {
            Car carToDelete = this.GetCarById(id);
            try
            {
                if (carToDelete != null)
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM \"Car\" WHERE \"Id\" = @IdValue";
                        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@IdValue", id);
                            command.ExecuteNonQuery();
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
            }
            return false;
        }
    }
}