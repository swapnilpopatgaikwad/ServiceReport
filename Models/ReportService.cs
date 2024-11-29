using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceReport.Enum;
using ServiceReport.Models;

namespace ServiceReport.Data;
public class ReportService : BaseModel
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
    public MachineType MachineType { get; set; }

    [Display(Name = "Service Type")]
    public ServiceType ServiceType { get; set; }

    [Display(Name = "Start Date")]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [Display(Name = "End Date")]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }

    [Display(Name = "Working Days")]
    public int WorkingDays { get; set; }

   

    [Display(Name = "Work Description")]
    [DataType(DataType.MultilineText)]  // To handle multiline input
    public string Description { get; set; }

    [Display(Name = "Diagnosis Details")]
    [DataType(DataType.MultilineText)]  // To handle multiline input
    public string Diagnosis { get; set; }

    [Display(Name = "Customer Feedback")]
    [DataType(DataType.MultilineText)]  // To handle multiline input
    public string CustomerFeedback { get; set; }

    public Status Status { get; set; }

    [Display(Name = "Spare Parts Used")]
    public string SpareParts { get; set; }

   [Display(Name = "Spare Part Status")]
[Required(ErrorMessage = "Please select a spare part status.")]
public SparePartStatus SparePartStatus { get; set; }


    [Display(Name = "Customer Seal & Sign")]
    public string CustomerSealSign { get; set; }

    [Display(Name = "Image")]
    public string ImageBase64 { get; set; }

    [Display(Name = "Token Number")]
    public string TokenNumber { get; set; }

    public virtual ICollection <ReportServiceArea> ReportServiceAreas { get; set; }= new HashSet<ReportServiceArea>();
}

