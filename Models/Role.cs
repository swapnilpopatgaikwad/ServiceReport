using ServiceReport.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ServiceReport.Models
{
    public class Role:BaseModel
    {
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public bool IsDefault { get; set; }
        [BindNever]
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
