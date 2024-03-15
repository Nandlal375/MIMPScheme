using Microsoft.AspNetCore.Routing.Constraints;

namespace WebApplication1.Models
{
    public class ReportModel
    {

        public IFormFile File { get; set; }

        public string FileName { get; set; }    
    }
}
