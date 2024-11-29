using Microsoft.AspNetCore.Mvc.ModelBinding;
using ServiceReport.Data;

namespace ServiceReport.Models
{
    public class ReportServiceArea : BaseModel
    {
       
        public int ReportServiceId { get; set; }
        [BindNever]
        public virtual ReportService ReportService { get; set; }
        public int AreaId { get; set; }
        [BindNever]
        public virtual Area Area { get; set; }
    }
}
