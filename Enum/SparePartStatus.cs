using System.ComponentModel.DataAnnotations;

namespace ServiceReport.Enum
{
    public enum SparePartStatus
    {
        [Display(Name = "FOC")]
        FOC,

        [Display(Name = "Settlement")]
        Settlement,

        [Display(Name = "Chargeable")]
        Chargeable,

        [Display(Name = "Returnable")]
        Returnable,

        [Display(Name = "Other")]
        Other,

        [Display(Name = "--NA--")]
        NA
    }

}
public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        var displayAttribute = enumValue.GetType()
            .GetField(enumValue.ToString())
            .GetCustomAttributes(typeof(DisplayAttribute), false)
            .FirstOrDefault() as DisplayAttribute;

        return displayAttribute != null ? displayAttribute.Name : enumValue.ToString();
    }
}
