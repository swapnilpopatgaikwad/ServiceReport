using ServiceReport.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using ServiceReport.Enum;

namespace ServiceReport.Models
{
    public class User : BaseModel
    {
        [Display(Name = "EmplyoeeID")]
        public string EmplyoeeID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Contact Number")]
        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number. Please enter 10 digits.")]
        public long ContactNumber { get; set; }


        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must be at least 8 characters long, contain at least one uppercase letter, one special character, and one numeric digit.")]
        public string Password { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Department")]
        public string Department { get; set; }


        [BindNever]
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        
    }
}
