using ServiceReport.Data;

namespace ServiceReport.Models
{
    public class Area: BaseModel
    {
        public string Name { get; set; }
        public int ReportServiceId {  get; set; }
        public ReportService ReportService { get; set; }

        // Navigation property for related Problems
        public virtual ICollection<Problem> Problems { get; set; } = new List<Problem>();
        public virtual ICollection<AreaProblem> AreaProblems { get; set; } = new List<AreaProblem>();

    }
}
