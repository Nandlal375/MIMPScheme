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
    public class ShowUserList
    {
        private readonly string _connectionString;
        private readonly IConfiguration Configuration;
        public ShowUserList(IConfiguration _configuration)
        {

            Configuration = _configuration;
            _connectionString = this.Configuration.GetConnectionString("DefaultConnection");
        }
        public List<User> UserList()
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("UserDetails", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            MySqlDataReader reader = command.ExecuteReader();
            var users = new List<User>();
            while (reader.Read())
            {
                users.Add(new User
                {
                    UserName = reader["user_name"].ToString(),
                    StateName = reader["statename"].ToString(),
                    DistrictName = reader["city"].ToString(),
                    LastLogin = reader["user_last_login"].ToString(),
                    User_level = reader["user_level"].ToString(),
                    Tender_status = reader["tender_status"].ToString(),
                    Status = reader["status"].ToString(),
                    UserId = reader["user_id"].ToString(),
                    name1 = reader["name1"].ToString(),
                    email1 = reader["email1"].ToString(),
                    desgination1 = reader["desgination1"].ToString(),
                    name2 = reader["name2"].ToString(),
                    email2 = reader["email2"].ToString(),
                    desgination2 = reader["desgination2"].ToString(),
                    name3 = reader["name3"].ToString(),
                    email3 = reader["email3"].ToString(),
                    desgination3 = reader["desgination3"].ToString(),
                    statevalue = reader["statevalue"].ToString()
                });
            }
            connection.Close();
            return users;
        }

    }
}
