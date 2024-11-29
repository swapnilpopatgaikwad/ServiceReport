using ServiceReport.Enum;
using System.ComponentModel.DataAnnotations;

namespace ServiceReport.Data
{
    public class DailyWorkReportViewModel
    {
        public string EmplyoeeID { get; set; }
        public string EngineerName { get; set; }
        public string CompanyName { get; set; }

        [Display(Name = "Machine Type")]
        [Required(ErrorMessage = "Please select a machine type.")]
        public MachineType MachineType { get; set; }
        public DateTime ReportDate { get; set; }
        [Required(ErrorMessage = "In Time is required.")]
        public TimeSpan? InTime { get; set; }
        [Required(ErrorMessage = "Out Time is required.")]
        public TimeSpan? OutTime { get; set; }
        public TimeSpan? WorkingHours { get; set; }
        public string WorkActivity { get; set; }
        public string CustomerRemark { get; set; }
        public string CustomerSealAndSign { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

}
