using Microsoft.AspNetCore.Mvc;
using MIMPScheme.Repository;
using MIMPScheme.Models;
using NuGet.Protocol.Core.Types;
using System.Diagnostics.Metrics;

namespace MIMPScheme.Controllers
{
    public class StudentController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration Configuration;
        private readonly IWebHostEnvironment _environment;
        public StudentController(ILogger<HomeController> logger, IConfiguration _configuration, IWebHostEnvironment environment)
        {
            _logger = logger;
            Configuration = _configuration;
            _environment = environment;
        }
        [HttpGet]
        public IActionResult Index(int Id = 0)
        {
            BindCountry();
            StudentModel list = new StudentModel();
            if (Id > 0)
            {
                ViewBag.btn = "Update";
                StudentRepo sr = new StudentRepo(Configuration);
                list = sr.GetStudentsById(Id);
                var HData1 = list.Hobby.ToString();
                string[] activityArray1 = HData1.Split(',');

                list.tblDepartments = new List<DeptDetail>
                {
                 new DeptDetail { DeptName = "Reading", ischecked = false },
                 new DeptDetail { DeptName = "Cooking", ischecked = false },
                 new DeptDetail { DeptName = "Cricket", ischecked = false }
                };
                foreach (var item in list.tblDepartments)
                {
                    foreach (var item1 in activityArray1)
                    {
                        if (item.DeptName == item1)
                        {
                            item.ischecked = true;
                            break;
                        }
                    }
                }
            }
            else
            {
            var HData = new List<string> { "Reading", "Cooking", "Cricket" };
            string HDataString = string.Join(", ", HData);
            string[] activityArray = HDataString.Split(',');
            list.tblDepartments = new List<DeptDetail>();
            for (int i = 0; i < activityArray.Length; i++)
            {
                list.tblDepartments.Add(new DeptDetail 
                { 
                 DeptName = activityArray[i],
                 ischecked = false,
                });
            }        
            }
            return View(list);
        }

        [HttpPost]
        public IActionResult Insert(StudentModel sm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            else
            {
                for (int i = 0; i < sm.Hobbys.Count(); i++)
                {
                    sm.Hobby += (sm.Hobbys[i] + ',').Trim(' ');
                }
                string filename = uploadFile(sm);
                var userdetail = new StudentModel()
                {
                    name = sm.name,
                    Address = sm.Address,
                    phonenumber = sm.phonenumber,
                    email = sm.email,
                    image = sm.image,
                    imageFileName = filename.Trim(' '),
                    Hobby = sm.Hobby,
                    country = sm.country,
                    Gender = sm.Gender
                };
                StudentRepo sr = new StudentRepo(Configuration);
                sr.StudentAdd(userdetail);
                TempData["message"] = "Record insert successffully !!";
                ModelState.Clear();
                return RedirectToAction("Details");
                }
        }

        public void BindCountry()
        {
            List<StudentModel> list = new List<StudentModel>();
            StudentRepo sr = new StudentRepo(Configuration);
            list = sr.AllCountry();
            ViewBag.Country = list;
        }

        public string uploadFile(StudentModel sdd)
		{
			string fileName = null;
			if (sdd.image != null)
			{
				string uploadDir = Path.Combine(_environment.WebRootPath, "Images");
				fileName = Guid.NewGuid().ToString() + "-" + sdd.image.FileName;
				string FilePath = Path.Combine(uploadDir, fileName);
				using (var filestream = new FileStream(FilePath, FileMode.Create))
				{
					sdd.image.CopyTo(filestream);
				}
			}
			return fileName;
		}

		public IActionResult Details(int pg = 1)
        {
            string message = TempData["message"] as string;
            ViewBag.image = TempData["filePath"] as string;
            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.Message = message;
            }
            else
            {
               //ViewBag.Message = "No message received!";
            }
            List<StudentModel> users = new List<StudentModel>();
            StudentRepo sr = new StudentRepo(Configuration);
            users = sr.GetStudents();
            const int pageSize = 5;
            if (pg < 1)
                pg = 1;
            int recsCount = users.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = users.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager; 

            //return View(users);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(StudentModel esm) 
        {
            StudentRepo sr = new StudentRepo(Configuration);
            for (int i = 0; i < esm.Hobbys.Count(); i++)
            {
                esm.Hobby += (esm.Hobbys[i] + ',').Trim(' ');
            }
            string filename = uploadFile(esm);
            var userdetail = new StudentModel()
            {
                name = esm.name,
                Address = esm.Address,
                phonenumber = esm.phonenumber,
                email = esm.email,
                Id = esm.Id,
                image = esm.image,
                imageFileName = filename,
                Hobby = esm.Hobby,
                country = esm.country,
                Gender = esm.Gender
            };
            if (sr.UpdateStudentsById(userdetail))
            {
                TempData["message"] = "Record updated successfully";
                return RedirectToAction("Details");
            }
            else 
            {
                return RedirectToAction("Index");
            }
        }
        public IActionResult Delete(int id, string imageFileName)
        {
            if (imageFileName == null)
            {
                return RedirectToAction("Details");
            }
            string webRootPath = Path.Combine(_environment.WebRootPath, "images");
            string imagePath = Path.Combine(webRootPath, imageFileName);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            StudentRepo sr = new StudentRepo(Configuration);
            sr.DeleteStudentsById(id);
            TempData["message"] = "Record delete successfully";
            return RedirectToAction("Details");
        }
    }
}
