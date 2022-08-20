// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Entities.Models
{
    public partial class UserLession
    {
        public int UserId { get; set; }
        public int LessionId { get; set; }

        public virtual RecommentLession Lession { get; set; }
        public virtual SysUser User { get; set; }
    }
}
