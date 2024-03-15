using System.ComponentModel.DataAnnotations.Schema;

namespace MIMPScheme.Models
{
    public class StudentModel
    {
        public string name { get; set; }
        public int Id { get; set; }
        public string Address { get; set; }
        public string phonenumber { get; set; }
        public string email { get; set; }
        public string? Selected { get; set; }
        public string country { get; set; }
        public string? Checkedproperties { get; set; }    
        public List<string> Hobbys { get; set; }
        public string? Hobby { get; set; }
        public string Gender { get; set; }
        public string? CId { get; set; }
        public string? CName { get; set; }
        public List<DeptDetail> tblDepartments { get; set; }
        public IFormFile image  { get; set; }
        [NotMapped]
        public string? imageFileName { get; set; }
    }
    public class DeptDetail
    {
        //public int DId { get; set; }
        public string DeptName { get; set; }
        public bool ischecked { get; set; }
    }
}
