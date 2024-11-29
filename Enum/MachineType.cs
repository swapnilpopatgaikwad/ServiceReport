using System.ComponentModel.DataAnnotations;

namespace ServiceReport.Enum
{
    public enum MachineType
    {
        [Display(Name = "Ecat Sheet Cutting")]
        EcatSheetCutting = 1,
        [Display(Name = "Backhoff Sheet Cutting")]
        BackhoffSheetCutting = 2,
        [Display(Name = "Fanuc Sheet Cutting")]
        FanucSheetCutting = 3,
        [Display(Name = "Chuck 3 Tube Cutting")]
        Chuck3TubeCutting = 4,
        [Display(Name = "Chuck 2 Tube Cutting")]
        Chuck2TubeCutting = 5,
        [Display(Name = "Sheet Tube Cutting Machine")]
        SheetTubeCuttingMachine = 6,
        [Display(Name = "Welding Machine")]
        WeldingMachine = 7,
        [Display(Name = "Marking Machine")]
        MarkingMachine = 8,
        [Display(Name = "Laser Cutting")]
        LaserCutting = 9,
        [Display(Name = "Ingraver Machine")]
        IngraverMachine = 10,
        [Display(Name = "Router Machine")]
        RouterMachine = 11,
        [Display(Name = "Robot Cutting Welding")]
        RobotCuttingWelding = 12,
        [Display(Name = "SPM")]
        SPM = 13,
        [Display(Name = "Other")]
        Other = 14
    }
}
