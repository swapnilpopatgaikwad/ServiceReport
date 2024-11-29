using Microsoft.AspNetCore.Mvc.ModelBinding;
using ServiceReport.Data;

namespace ServiceReport.Models
{
    public class AreaProblem:BaseModel
    {
        public int AreaId { get; set; }
        [BindNever]
        public virtual Area Area { get; set; }
        public int ProblemId { get; set; }
        [BindNever]
        public virtual Problem Problem { get; set; }
    }
}
