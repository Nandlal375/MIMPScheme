using Microsoft.AspNetCore.Mvc;
using MIMPScheme.Repository;
using System.Drawing;
using System.Text;
//using static System.Net.Mime.MediaTypeNames;

namespace MIMPScheme.Controllers
{
    public class AdminController : Controller
    {
        private const string SessionCaptcha = "_Captcha";
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration Configuration;
        public AdminController(ILogger<HomeController> logger, IConfiguration _configuration)
        {
            _logger = logger;
            Configuration = _configuration;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        private string GetRandomText()
        {
            StringBuilder randomText = new StringBuilder();
            string alphabets = "012345679ACEFGHKLMNPRSWXZabcdefghijkhlmnopqrstuvwxyz";
            Random r = new Random();
            for (int j = 0; j <= 5; j++)
            {
                randomText.Append(alphabets[r.Next(alphabets.Length)]);
            }
            return randomText.ToString();
        }

        //public ActionResult CustomCaptcha()
        //{

        //    HttpContext.Session.SetString(SessionCaptcha, GetRandomText());
        //    //string captcha = GetRandomText();
        //    //Session["CAPTCHA"] = GetRandomText();
        //    return View();
        //}

        public FileResult GetCaptchaImage()
        {
            string captcha = GetRandomText();
            HttpContext.Session.SetString(SessionCaptcha, GetRandomText());
            string text = HttpContext.Session.GetString(SessionCaptcha);

            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            Font font = new Font("Arial", 15);
            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width + 40, (int)textSize.Height + 20);
            drawing = Graphics.FromImage(img);

            Color backColor = Color.SeaShell;
            Color textColor = Color.Red;
            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, 20, 10);

            drawing.Save();

            font.Dispose();
            textBrush.Dispose();
            drawing.Dispose();

            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            img.Dispose();

            return File(ms.ToArray(), "image/png");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomCaptcha(IFormCollection form)
        {
            string clientCaptcha = form["txt_letters_code"];
            string serverCaptcha = HttpContext.Session.GetString(SessionCaptcha);

            if (!clientCaptcha.Equals(serverCaptcha))
            {
                ViewBag.ShowCAPTCHA = serverCaptcha;

                ViewBag.CaptchaError = "Sorry, please write exact text as written above.";
                return View("Login");
            }

            // go ahead and validate username and password
            string userName = form["txtUserName"];
            string password = form["txtPassword"];
            LoginRepository objlogin = new LoginRepository(Configuration); ;
           
            objlogin.ValidateUser(userName, password);

            return RedirectToAction("ForgotPassword");
        }
        public bool validPassword(string strPass)
        {
            var rpassword = strPass;
            var re = @" / ^(?=.*\d)(?=.* [a - z])(?=.* [A - Z]).{ 8,28}$/";
            if (!re.Contains(rpassword))
            {
                ViewBag.CaptchaError = "Your password must satisfy the following. \n\n* Password should be 4 to 8 character long. \n* Password should have at least one alphabet. \n* Password should have at least one numeric value. \n* Password should  have special characters.";
                return true;
            }
            return true;

        }
    }
}
