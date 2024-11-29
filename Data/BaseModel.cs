using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace ServiceReport.Data
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
        [BindNever]
        public DateTime CreatedDate { get; set; }
        [BindNever]
        public DateTime ModifiedDate { get; set; }
    }
}
