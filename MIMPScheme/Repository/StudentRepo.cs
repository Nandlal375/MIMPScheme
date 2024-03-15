using MIMPScheme.Models;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using MIMPScheme.Controllers;
using MIMPScheme.Data.Helper;
using MySqlConnector;
using NuGet.Protocol.Core.Types;
using System.Security.Cryptography.X509Certificates;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
namespace MIMPScheme.Repository
{
    public class StudentRepo
    {
        private readonly string _connectionString;
        private readonly IConfiguration Configuration;
        public StudentRepo(IConfiguration _configuration)
        {

            Configuration = _configuration;
            _connectionString = this.Configuration.GetConnectionString("DefaultConnection");

        }

        public void StudentAdd(StudentModel sm)
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("AddStudent", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@name", sm.name);
            command.Parameters.AddWithValue("@Address", sm.Address);
            command.Parameters.AddWithValue("@phonenumber", sm.phonenumber);
            command.Parameters.AddWithValue("@email", sm.email);
            command.Parameters.AddWithValue("@image", sm.imageFileName);
            command.Parameters.AddWithValue("@country", sm.country);
            command.Parameters.AddWithValue("@Hobby", sm.Hobby);
            command.Parameters.AddWithValue("@Gender", sm.Gender);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public List<StudentModel> GetStudents()
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("ViewStudent", connection);
            command.CommandType = CommandType.StoredProcedure;
            MySqlDataReader reader = command.ExecuteReader();
            var users = new List<StudentModel>();
            while (reader.Read())
            {
                users.Add(new StudentModel
                {
                    name = reader["name"].ToString(),
                    Address = reader["Address"].ToString(),
                    phonenumber = reader["phonenumber"].ToString(),
                    email = reader["email"].ToString(),
                    imageFileName = reader["image"].ToString(),
                    Id = Convert.ToInt32(reader["Id"]),
                    country = reader["country"].ToString(),
                    Hobby = reader["Hobby"].ToString(),
                    Gender = reader["Gender"].ToString()
                });
            }
            connection.Close();
            return users;
        }

        public StudentModel GetStudentsById(int id)
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();
            using MySqlCommand command = new MySqlCommand("SELECT * FROM person WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            MySqlDataReader reader = command.ExecuteReader();
            var users = new StudentModel();
            while (reader.Read())
            {
                //users.name = reader["name"].ToString()
                //users.Add(new StudentModel
                //{
                users.name = reader["name"].ToString();
                users.Address = reader["Address"].ToString();
                users.phonenumber = reader["phonenumber"].ToString();
                users.email = reader["email"].ToString();
                users.imageFileName = reader["image"].ToString();
                users.Id = Convert.ToInt32(reader["Id"]);
                users.country = reader["country"].ToString();
                users.Selected = reader["country"].ToString();
                users.Gender = reader["Gender"].ToString();
                users.Hobby = reader["Hobby"].ToString();
                users.Checkedproperties = reader["Gender"].ToString();
                //});
            }
            connection.Close();
            return users;
        }

        public List<StudentModel> AllCountry()
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();
            using MySqlCommand command = new MySqlCommand("select * from Country", connection);
            MySqlDataReader reader = command.ExecuteReader();
            var getitems = new List<StudentModel>();
            while (reader.Read())
            {
                getitems.Add(new StudentModel
                {
                    CId = reader["cid"].ToString(),
                    CName = reader["countryname"].ToString()
                });
            }
            connection.Close();
            return getitems;
        }

        public void DeleteStudentsById(int id)
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();
            using MySqlCommand command = new MySqlCommand("DELETE FROM person WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public bool UpdateStudentsById(StudentModel esm)
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("ModifyStudent", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@name1", esm.name);
            command.Parameters.AddWithValue("@Address1", esm.Address);
            command.Parameters.AddWithValue("@phonenumber1", esm.phonenumber);
            command.Parameters.AddWithValue("@email1", esm.email);
            command.Parameters.AddWithValue("@image1", esm.imageFileName);
            command.Parameters.AddWithValue("@country1", esm.country);
            command.Parameters.AddWithValue("@Hobby1", esm.Hobby);
            command.Parameters.AddWithValue("@Gender1", esm.Gender);
            command.Parameters.AddWithValue("@Idd", esm.Id);
            if (esm.Id > 0)
            {
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            else
            {
                connection.Close();
                return false;
            }
        }
    }
}
