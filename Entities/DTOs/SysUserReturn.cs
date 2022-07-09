using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class SysUserReturn
    {
        public int UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public string ImagePath { get; set; }
        public bool? IsLocked { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
