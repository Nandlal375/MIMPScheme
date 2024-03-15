using Microsoft.AspNetCore.Mvc;
using MIMPScheme.Models;
using MIMPScheme.Repository;
using System.Configuration;
using System.Diagnostics;
using WebApplication1.Models;

namespace MIMPScheme.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration Configuration;
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment, IConfiguration configuration)
        {
            _logger = logger;
            _environment = environment;
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            //return RedirectToAction("ShowDownLoadFile");
            //return View("ShowDownLoadFile");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult upload()
        {
            return View();
        }
        public IActionResult ShowDownLoadFile()
        {
            List<ReportModel> list = new List<ReportModel>();
            FileUpload fur = new FileUpload(Configuration);
            list = fur.uploadFileshow();
            return View(list);
        }

        public IActionResult Download(string fileName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", fileName);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }
            string contentType = "application/pdf";
            string downloadFileName = fileName;
            return File(new FileStream(filePath, FileMode.Open, FileAccess.Read), contentType, downloadFileName);
        }

            [HttpPost]
        public async Task<IActionResult> Upload(ReportModel model)
        {
            foreach (var file in Request.Form.Files)
            {
                if (file.Length > 0)
                {
                    var filePath = Path.Combine(_environment.ContentRootPath, "uploads", file.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    FileUpload fu = new FileUpload(Configuration);
                    fu.uploadFile(model);
                }
            }
            return RedirectToAction("ShowDownLoadFile");
        }
    }
}
