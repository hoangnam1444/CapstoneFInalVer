using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class UserMajor
    {
        public int MajorId { get; set; }
        public int UserId { get; set; }

        public virtual SysUser User { get; set; }
        public virtual Majors Major { get; set; }
    }
}
