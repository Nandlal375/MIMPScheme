using MIMPScheme.Models;
using Microsoft.AspNetCore.Mvc;
using MIMPScheme.Controllers;
using MIMPScheme.Data.Helper;
using MySqlConnector;
using NuGet.Protocol.Core.Types;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MIMPScheme.Repository
{
    public class AddState
    {
        private readonly string _connectionString;
        private readonly IConfiguration Configuration;
        public AddState(IConfiguration _configuration)
        {

            Configuration = _configuration;
            _connectionString = this.Configuration.GetConnectionString("DefaultConnection");
        }

        public List<StateAdd_User> AddStateShow()
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("AddState", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            MySqlDataReader reader = command.ExecuteReader();
            var states = new List<StateAdd_User>();
            while (reader.Read())
            {
                states.Add(new StateAdd_User
                {
                    Id = reader["id"].ToString(),
                    StateName = reader["name"].ToString()
                });
            }
            
            connection.Close();
            return states;
           
        }

    }
}
