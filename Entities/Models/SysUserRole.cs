using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Entities.Models
{
    public partial class SysUserRole
    {
        public SysUserRole()
        {
            SysUser = new HashSet<SysUser>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<SysUser> SysUser { get; set; }
    }
}
