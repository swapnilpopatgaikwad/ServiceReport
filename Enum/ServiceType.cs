using System.ComponentModel.DataAnnotations;

namespace ServiceReport.Enum
{
    public enum ServiceType
    {
        [Display(Name = "In Warranty")]
        InWarranty,
        [Display(Name = "Out of Warranty (Paid Service)")]
        OutOfWarrantyPaid,
        [Display(Name = "Out of Warranty (Free of Cost)")]
        OutOfWarrantyFree,

        AMC,

        [Display(Name = "Preventive Maintenance Visit")]
        PreventiveMaintenanceVisit,

        Other
    }

}
