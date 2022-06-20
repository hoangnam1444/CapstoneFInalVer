using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class SecurityCode
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Code { get; set; }

        public virtual SysUser user { get; set; }
    }
}
