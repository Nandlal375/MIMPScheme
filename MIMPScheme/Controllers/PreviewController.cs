using Microsoft.AspNetCore.Mvc;

namespace MIMPScheme.Controllers
{
    public class PreviewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
