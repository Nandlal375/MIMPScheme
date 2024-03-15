using MIMPScheme.Models;
using System.Data;
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
    public class ModifyUserRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration Configuration;
        public ModifyUserRepository(IConfiguration _configuration)
        {

            Configuration = _configuration;
            _connectionString = this.Configuration.GetConnectionString("DefaultConnection");

        }
        public bool ModifyUserDetails(Add_User aus)
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("ModifyUserDetails", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@username", aus.username);
            command.Parameters.AddWithValue("@password", aus.password);
            command.Parameters.AddWithValue("@stateId", aus.stateId);
            string districtId = "";
            for (int i = 0; i < aus.districId.Length; i++)
            {
                if (i == 0)
                {
                    districtId += aus.districId[i];
                }
                else
                {
                    districtId += "," + aus.districId[i];
                }
            }

            command.Parameters.AddWithValue("@districId", districtId);
            command.Parameters.AddWithValue("@Fname", aus.Fname);
            command.Parameters.AddWithValue("@Femail", aus.Femail);
            command.Parameters.AddWithValue("@Fdesignation", aus.Fdesignation);
            command.Parameters.AddWithValue("@Sname", aus.Sname);
            command.Parameters.AddWithValue("@Semail", aus.Semail);
            command.Parameters.AddWithValue("@Sdesignation", aus.Sdesignation);
            command.Parameters.AddWithValue("@Tname", aus.Tname);
            command.Parameters.AddWithValue("@Temail", aus.Temail);
            command.Parameters.AddWithValue("@Tdesignation", aus.Tdesignation);
            command.Parameters.AddWithValue("@user_idd", aus.user_idd);
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
