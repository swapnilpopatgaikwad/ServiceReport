using ServiceReport.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ServiceReport.Models
{
    public class UserRole:BaseModel
    {
        public int UserId { get; set; }
        [BindNever]
        public virtual User User { get; set; }
        public int RoleId { get; set; }
        [BindNever]     
        public virtual Role Role { get; set; }
       
    }
}
