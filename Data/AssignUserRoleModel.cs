using ServiceReport.Models;
using System.ComponentModel.DataAnnotations;

namespace ServiceReport.Data
{
    public class AssignUserRoleModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int RoleId { get; set; }
    }
}
