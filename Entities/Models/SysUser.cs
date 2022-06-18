using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Entities.Models
{
    public partial class SysUser
    {
        public SysUser()
        {
            TestResults = new HashSet<TestResults>();
            UserLearningPath = new HashSet<UserLearningPath>();
            VcGuidance = new HashSet<VcGuidance>();
        }

        public int UserId { get; set; }
        public string FullName { get; set; }
        public bool? Gender { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? BirthDay { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string ImagePath { get; set; }
        public bool? IsLocked { get; set; }
        public int? Grade { get; set; }
        public float? Gpa10 { get; set; }
        public float? Gpa11 { get; set; }
        public float? Gpa12 { get; set; }
        public int? AdminIdUpdate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual SysUser UpdateAdmin { get; set; }
        public virtual SysUserRole Role { get; set; }
        public virtual ICollection<TestResults> TestResults { get; set; }
        public virtual ICollection<UserLearningPath> UserLearningPath { get; set; }
        public virtual ICollection<VcGuidance> VcGuidance { get; set; }
    }
}
