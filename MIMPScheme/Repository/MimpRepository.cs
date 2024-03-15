//using MySql.Data.MySqlClient;
//using System;
//using System.Collections.Generic;
//using System.Data;

using MIMPScheme.Models;
using MySqlConnector;

namespace MIMPScheme.Repository
{
    public class MimpRepository
    {
        private readonly string _connectionString;

        public MimpRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Mimp> GetAllMimps()
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            using MySqlCommand command = new MySqlCommand("SELECT * FROM Mimps", connection);
            using MySqlDataReader reader = command.ExecuteReader();

            var Mimps = new List<Mimp>();
            //while (reader.Read())
            //{
            //    Mimps.Add(new Mimp
            //    {
            //        Id = Convert.ToInt32(reader["Id"]),
            //        Title = reader["Title"].ToString(),
            //        IsCompleted = Convert.ToBoolean(reader["IsCompleted"])
            //    });
            //}

            return Mimps;
        }

        public Mimp GetMimpById(int id)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            using MySqlCommand command = new MySqlCommand("SELECT * FROM Mimps WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);

            using MySqlDataReader reader = command.ExecuteReader();

            //if (reader.Read())
            //{
            //    return new Mimp
            //    {
            //        Id = Convert.ToInt32(reader["Id"]),
            //        Title = reader["Title"].ToString(),
            //        IsCompleted = Convert.ToBoolean(reader["IsCompleted"])
            //    };
            //}

            return null;
        }

        public void AddMimp(Mimp Mimp)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            using MySqlCommand command = new MySqlCommand("INSERT INTO Mimps (Title, IsCompleted) VALUES (@Title, @IsCompleted)", connection);
            //command.Parameters.AddWithValue("@Title", Mimp.Title);
            //command.Parameters.AddWithValue("@IsCompleted", Mimp.IsCompleted);

            command.ExecuteNonQuery();
        }

        public void UpdateMimp(Mimp updatedMimp)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            using MySqlCommand command = new MySqlCommand("UPDATE Mimps SET Title = @Title, IsCompleted = @IsCompleted WHERE Id = @Id", connection);
            //command.Parameters.AddWithValue("@Id", updatedMimp.Id);
            //command.Parameters.AddWithValue("@Title", updatedMimp.Title);
            //command.Parameters.AddWithValue("@IsCompleted", updatedMimp.IsCompleted);

            command.ExecuteNonQuery();
        }

        public void DeleteMimp(int id)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            using MySqlCommand command = new MySqlCommand("DELETE FROM Mimps WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }
    }

}
