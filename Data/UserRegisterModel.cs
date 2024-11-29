using ServiceReport.Enum;
using System.ComponentModel.DataAnnotations;

namespace ServiceReport.Data
{
    public class UserRegisterModel
    {
       
            [Display(Name = "EmplyoeeID")]
        [Required(ErrorMessage = "EmplyoeeID is required")]
        public string EmplyoeeID { get; set; }

            [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

            [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

            [Display(Name = "Contact Number")]
        [Required(ErrorMessage = "Contact Number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number. Please enter 10 digits.")]
            public long ContactNumber { get; set; }


            [Display(Name = "Password")]
            [Required(ErrorMessage = "Password is required")]
            [RegularExpression(@"^(?=.*[A-Z])(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must be at least 8 characters long, contain at least one uppercase letter, one special character, and one numeric digit.")]
            public string Password { get; set; }

            [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

            [Display(Name = "Department")]
        [Required(ErrorMessage = "Department is required")]
        public string Department { get; set; }
    }
}
