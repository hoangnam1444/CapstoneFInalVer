using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class User_Blog
    {
        public int UserId { get; set; }
        public int BlogId { get; set; }
        public bool IsOwner { get; set; }
        public bool IsReacted { get; set; }

        public SysUser User { get; set; }
        public Blog Blog { get; set; }

    }
}
