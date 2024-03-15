using Microsoft.AspNetCore.Mvc;
using MIMPScheme.Controllers;
using MIMPScheme.Data.Helper;
using MIMPScheme.Models;
using MySqlConnector;
using NuGet.Protocol.Core.Types;

namespace MIMPScheme.Repository
{
    public class LoginRepository
    {
        private readonly string  _connectionString;
        private readonly ILogger<LoginRepository> _logger;
        private readonly IConfiguration Configuration;
        public LoginRepository(IConfiguration _configuration)
        {
           // _logger = logger;
            Configuration = _configuration;
            _connectionString= this.Configuration.GetConnectionString("DefaultConnection");
        }
        //public LoginRepository() 
        //{
         
        //    _connectionString = Configuration.GetConnectionString("DefaultConnection"); ;
        // }
               
        private string GetConnectionString()
        {
            return this.Configuration.GetConnectionString("DefaultConnection");
        }
        public Mimp ValidateUser(string username,string password)
        {
            //string _connectionString = this.Configuration.GetConnectionString("DefaultConnection");
             MySqlConnection connection = new MySqlConnection(_connectionString);            
           
            //string str=   GetConnectionString();
            string userpassword=Security.HashSHA1(password);   

            MySqlParameter[] commandParameters = new MySqlParameter[2];
            commandParameters[0] = new MySqlParameter("@username", username);
            commandParameters[1] = new MySqlParameter("@userpassword", userpassword);
            MySqlDataReader dr;
            dr=MySqlHelperClass.ExecuteReader(connection, "ValidateUser", commandParameters);           

            if (dr.Read())
            {
                return new Mimp
                {
                  
                    username = dr["user_name"].ToString(),
                    role = Convert.ToInt32(dr["user_level"])
                };
            }

            return null;
        }
    }
}
