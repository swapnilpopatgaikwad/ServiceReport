using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace ServiceReport.Data
{
    public class UserRoleModel
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int RoleId { get; set; }

        [BindNever]
        public string UserName { get; set; }
        [BindNever]
        public string RoleName { get; set; }
        [BindNever]
        public DateTime CreatedDate { get; set; }
        [BindNever]
        public DateTime ModifiedDate { get; set; }
    }
}
