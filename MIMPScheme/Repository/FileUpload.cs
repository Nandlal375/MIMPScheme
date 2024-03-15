using MIMPScheme.Models;
using Microsoft.AspNetCore.Mvc;
using MIMPScheme.Controllers;
using MIMPScheme.Data.Helper;
using MySqlConnector;
using NuGet.Protocol.Core.Types;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Net;
using WebApplication1.Models;

namespace MIMPScheme.Repository
{
    public class FileUpload
    {
        private readonly string _connectionString;
        private readonly IConfiguration Configuration;
        public FileUpload(IConfiguration _configuration)
        {

            Configuration = _configuration;
            _connectionString = this.Configuration.GetConnectionString("DefaultConnection");

        }
        public void uploadFile(ReportModel rm)
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("FileUpload", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@FileName", rm.File.FileName);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public List<ReportModel> uploadFileshow()
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("FileUploadRetrieve", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            MySqlDataReader reader = command.ExecuteReader();
            var rp = new List<ReportModel>();
            while (reader.Read())
            {
                rp.Add(new ReportModel
                {
                    FileName = reader["fillename"].ToString()
                });
            }
            connection.Close();
            return rp;
        }
    }
}
