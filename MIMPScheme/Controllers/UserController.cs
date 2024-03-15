using Microsoft.AspNetCore.Mvc;
using MIMPScheme.Controllers;
using MIMPScheme.Data.Helper;
using MIMPScheme.Models;
using MIMPScheme.Repository;
using MySqlConnector;
using NuGet.Protocol.Core.Types;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MIMPScheme.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration Configuration;
        public UserController(ILogger<HomeController> logger, IConfiguration _configuration)
        {
            _logger = logger;
            Configuration = _configuration;
        }
       

        public IActionResult Index()
        { 
          return View();
        }
        public JsonResult UserList()
        {
            List<User> users = new List<User>();
            ShowUserList showUserList = new ShowUserList(Configuration);
            users = showUserList.UserList();
            return Json(users);

        }

        public JsonResult AddState()
        {
            List<StateAdd_User> state = new List<StateAdd_User>();
            AddState addState = new AddState(Configuration);
           state = addState.AddStateShow();
            return Json(state);
        }

        public JsonResult BindDistrict(DistrictFindByState dfs1)
        {
            List<DistrictFindByState> ss = new List<DistrictFindByState>();
            var sv = new DistrictFindByState()
            {
                userId = dfs1.userId
            };
            FindStateRepo fsr = new FindStateRepo(Configuration);
            ss = fsr.StateVal(sv);
            return Json(ss);
        }

        public JsonResult BindDistrict1(DistrictFindByState dfs)
        {
            List<DistrictFindByState> ss1 = new List<DistrictFindByState>();
            var sv1 = new DistrictFindByState()
            {
                userId = dfs.userId
            };
            FindStateRepo fsr1 = new FindStateRepo(Configuration);
            ss1 = fsr1.StateVal1(sv1);
            return Json(ss1);
        }

        [HttpPost]
        public JsonResult UserStatus(User_Status us)
        {
            var status = new User_Status()
            {
                statusid = us.statusid,
                userId = us.userId
            };
            UserStatusRepository usr = new UserStatusRepository(Configuration);
            usr.UserStatus(status);
            return Json(status);
        }
        [HttpPost]
        public bool AddStateUser(AddState_User addState)
        {
            string strpass = encryptpass(addState.password);
            var user = new AddState_User()
            {
                username = addState.username,
                password = strpass,
                stateValue = addState.stateValue
               
            };
            AddStateRepository addStateRepository = new AddStateRepository(Configuration);
            if (addStateRepository.AddStateDetail(user))
            { 
             return true;
            }
            else
            {
                return false;
            }

        }

        public string encryptpass(string password)
        {
            string msg = "";
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            msg = Convert.ToBase64String(encode);
            return msg;
        }
        [HttpPost]
        public bool AddUserDetail(Add_User au)
        {
            string strpass = encryptpass(au.password);
            var userdetail = new Add_User()
            {
                stateId = au.stateId,
                districId = au.districId,
                username = au.username,
                password = strpass,
                Fname = au.Fname,
                Femail = au.Femail,
                Fdesignation = au.Fdesignation,
                Sname = au.Sname,
                Semail = au.Semail,
                Sdesignation = au.Sdesignation,
                Tname = au.Tname,
                Temail = au.Temail,
                Tdesignation = au.Tdesignation
            };
            

            AddUserRepo aur = new AddUserRepo(Configuration);
            if (aur.AddUserDetails(userdetail))
            {
                return true;
            }
            else
            {

               return false;
            }
        }

        //public void BindDistrictWithSelectedItem(int userId) 
        //{
        //    sp_DistrictGetById


        //}

        [HttpPost]
        public bool AddModifyUserDetail(Add_User au)
        {
            string strpass = encryptpass(au.password);
            var userdetail = new Add_User()
            {
                stateId = au.stateId,
                districId = au.districId,
                username = au.username,
                password = strpass,
                Fname = au.Fname,
                Femail = au.Femail,
                Fdesignation = au.Fdesignation,
                Sname = au.Sname,
                Semail = au.Semail,
                Sdesignation = au.Sdesignation,
                Tname = au.Tname,
                Temail = au.Temail,
                Tdesignation = au.Tdesignation,
                user_idd = au.user_idd
            };
             ModifyUserRepository mud = new ModifyUserRepository(Configuration);
            if (mud.ModifyUserDetails(userdetail))
            {
                return true;
            }
            else
            {

                return false;
            }
        }
    }
}
