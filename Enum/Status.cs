using System.ComponentModel.DataAnnotations;

namespace ServiceReport.Enum
{
    public enum Status
    {
        [Display(Name = "In Progress")]
        InProgress,

        [Display(Name = "Completed")]
        Completed,

        [Display(Name = "On Hold")]
        OnHold,

        [Display(Name = "Cancelled")]
        Cancelled
    }


}
