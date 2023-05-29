using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Npgsql;

namespace FootballClub.Staff.Database_Logic
{
    public class DatabaseHandler
    {
        private string connectionString = "Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=password";
        //"Server,Port,User Id,Password,Database"
        public void InsertName(string name)
        {
            using(NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO \"personnamesss\" (PersonName) VALUES (@Value)";
                using(NpgsqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@Value", name);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public int GetName()
        {
            int lastItemId = 0;
            using(NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                //string query = "SELECT Id FROM personnamesss ORDER BY Id DESC LIMIT 1;";
                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    using(NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        //if(reader.HasRows)
                        while (reader.Read())
                        {
                            //(int)reader["Id"];
                        }
                        if (reader.Read())
                        {
                            lastItemId = reader.GetInt32(0);
                        }
                    }
                }
                //using zatvori vezu bez da koristim close()
                //connection.Close();
            }
            return lastItemId;
        }
    }
}