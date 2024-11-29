using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceReport.Enum;
using System.ComponentModel.DataAnnotations;

namespace ServiceReport.Data
{
    public class ReportServiceViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Employee ID")]
        public string EmployeeID { get; set; }

        [Display(Name = "Engineer Name")]
        public string EngineerName { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        //[Display(Name = "Other Company Name")]
        //public string OtherCompanyName { get; set; }

        [Display(Name = "Machine Serial Number")]
        public string MachineSerialNumber { get; set; }

        [Display(Name = "Laser Power")]
        public string LaserPower { get; set; }

        [Display(Name = "Company Address")]
        public string CompanyAddress { get; set; }

        [Display(Name = "Machine Type")]
        [Required(ErrorMessage = "Please select a machine type.")]
        public MachineType MachineType { get; set; }


        [Display(Name = "Service Type")]
        [Required(ErrorMessage = "Please select a service type.")]
        public ServiceType ServiceType { get; set; }


        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Working Days")]
        public int WorkingDays { get; set; }

        //[Display(Name = "Selected Areas")]
        //public List<int> SelectedAreas { get; set; }

        //public List<SelectListItem> AreaOptions { get; set; } // Dropdown options
                public List<int> SelectedAreas { get; set; }
        public List<SelectListItem> AreaOptions { get; set; }

        // New property to hold problems for each selected area
        public Dictionary<int, List<string>> SelectedProblems { get; set; } = new Dictionary<int, List<string>>();
        [Display(Name = "Work Description")]
        [DataType(DataType.MultilineText)]  // To handle multiline input
        public string Description { get; set; }

        [Display(Name = "Diagnosis Details")]
        [DataType(DataType.MultilineText)]  // To handle multiline input
        public string Diagnosis { get; set; }

        [Display(Name = "Customer Feedback")]
        [DataType(DataType.MultilineText)]  // To handle multiline input
        public string CustomerFeedback { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Please select a status.")]
        public Status Status { get; set; }

        [Display(Name = "Spare Parts Used")]
        public string SpareParts { get; set; }

       [Display(Name = "Spare Part Status")]
[Required(ErrorMessage = "Please select a spare part status.")]
public SparePartStatus SparePartStatus { get; set; }


        [Display(Name = "Customer Name Seal & Sign")]
        public string CustomerSealSign { get; set; }

        [Display(Name = "Customer Name Seal & Sign")]
        public string ImageBase64 { get; set; }

        //[Display(Name = "Token Number")]
        //public string TokenNumber { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
