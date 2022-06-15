using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Entities.Models
{
    public partial class VcGuidance
    {
        public int UserId { get; set; }
        public int CollegeId { get; set; }
        public int MajorId { get; set; }

        public virtual Colleges College { get; set; }
        public virtual Majors Major { get; set; }
        public virtual SysUser User { get; set; }
    }
}
