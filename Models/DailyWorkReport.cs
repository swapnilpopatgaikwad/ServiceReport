using ServiceReport.Data;
using ServiceReport.Enum;
using System.ComponentModel.DataAnnotations;

namespace ServiceReport.Models
{
    public class DailyWorkReport: BaseModel
    {
        [Required]
        [Display(Name = "Employee ID")]
        public string EmplyoeeID { get; set; }

        [Required]
        [Display(Name = "Engineer Name")]
        public string EngineerName { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }


        [Display(Name = "Machine Type")]
        [Required(ErrorMessage = "Please select a machine type.")]
        public MachineType MachineType { get; set; }

        [Required]
        [Display(Name = "Report Date")]
        [DataType(DataType.Date)]  // To ensure a date picker is displayed
        public DateTime ReportDate { get; set; }

        [Required]
        [Display(Name = "In Time")]
        [DataType(DataType.Time)]  // To ensure a time picker is displayed
        public TimeSpan? InTime { get; set; }

        [Required]
        [Display(Name = "Out Time")]
        [DataType(DataType.Time)]  // To ensure a time picker is displayed
        public TimeSpan? OutTime { get; set; }

        [Display(Name = "Working Hours")]
        public TimeSpan? WorkingHours { get; set; }

        [Required]
        [Display(Name = "Work Activity")]
        public string WorkActivity { get; set; }

        [Display(Name = "Customer Remark")]
        public string CustomerRemark { get; set; }

        [Display(Name = "Customer Seal & Signature")]
        public string CustomerSealAndSign { get; set; }

        //[Display(Name = "Token Number")]
        //public string TokenNumber { get; set; }
    }
}
