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

namespace MIMPScheme.Repository
{
    public class FindStateRepo
    {
        private readonly string _connectionString;
        private readonly IConfiguration Configuration;
        public FindStateRepo(IConfiguration _configuration)
        {

            Configuration = _configuration;
            _connectionString = this.Configuration.GetConnectionString("DefaultConnection");

        }

        public List<DistrictFindByState> StateVal(DistrictFindByState dfs)
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("Districtdetail1", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@stateid", dfs.userId);
            MySqlDataReader reader = command.ExecuteReader();
            var svf = new List<DistrictFindByState>();
            while (reader.Read())
            {
                svf.Add(new DistrictFindByState
                {
                    userId = reader["id"].ToString(),
                    stateName = reader["state"].ToString(),
                    districName = reader["name"].ToString()
                });
            }
            connection.Close();
            return svf;
        }


        public List<DistrictFindByState> StateVal1(DistrictFindByState dfs)
        {
            MySqlConnection connection1 = new MySqlConnection(_connectionString);
            connection1.Open();
            MySqlCommand command = new MySqlCommand("Districtdetail1", connection1);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@stateid", dfs.userId);
            MySqlDataReader reader = command.ExecuteReader();
            var svf1 = new List<DistrictFindByState>();
            while (reader.Read())
            {
                svf1.Add(new DistrictFindByState
                {
                    userId = reader["id"].ToString(),
                    stateName = reader["state"].ToString(),
                    districName = reader["name"].ToString()
                });
            }
            connection1.Close();
            return svf1;
        }


    }
}
