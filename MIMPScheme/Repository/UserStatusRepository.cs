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
    public class UserStatusRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration Configuration;
        public UserStatusRepository(IConfiguration _configuration)
        {

            Configuration = _configuration;
            _connectionString = this.Configuration.GetConnectionString("DefaultConnection");
        }

        public void UserStatus(User_Status userStatus)
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();
            using MySqlCommand command = new MySqlCommand("ChangeUserStatus", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@statusid", userStatus.statusid);
            command.Parameters.AddWithValue("@userId", userStatus.userId);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
