using Microsoft.AspNetCore.Mvc.ModelBinding;
using ServiceReport.Data;

namespace ServiceReport.Models
{
    public class Problem : BaseModel
    {
        public string ProblemName { get; set; }
        // Foreign key to Area
        public int AreaId { get; set; }  // This establishes the foreign key relationship
        [BindNever]
        public virtual Area Area { get; set; }
    }
}
