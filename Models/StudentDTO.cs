using CollegeApp.Validators;
using System.ComponentModel.DataAnnotations;
namespace CollegeApp.Models
{
    public class StudentDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Student name is Required")]
        [StringLength(20)]
        public string StudentName { get; set; }
        public string Address { get; set; }
        [EmailAddress(ErrorMessage ="Please Enter valid Email Address")]
        public string Email { get; set; }
        //[DateCheckAttribute]//custom validator
        //public DateTime AdmitionDate { get; set; }
    }
}
