using Microsoft.AspNetCore.Mvc;
using MIMPScheme.Controllers;
using MIMPScheme.Data.Helper;
using MIMPScheme.Models;
using MySqlConnector;
using NuGet.Protocol.Core.Types;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MIMPScheme.Repository
{
    public class AddStateRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration Configuration;
        public AddStateRepository(IConfiguration _configuration)
        {

            Configuration = _configuration;
            _connectionString = this.Configuration.GetConnectionString("DefaultConnection");
        }

        public bool AddStateDetail(AddState_User _st)
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("InsertAddStateUser", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@name", _st.username);
            command.Parameters.AddWithValue("@stateValue", _st.stateValue);
            command.Parameters.AddWithValue("@password", _st.password);
            MySqlDataReader reader = command.ExecuteReader();
            {
                if (reader.HasRows)
                {
                    connection.Close();
                    return true;
                }
                else
                {
                    reader.DisposeAsync();
                    command.ExecuteNonQuery();
                    connection.Close();
                    return false;
                }
            }
        }
    }
}
