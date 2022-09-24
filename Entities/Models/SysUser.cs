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
            UserLession = new HashSet<UserLession>();
            VcGuidance = new HashSet<VcGuidance>();
            SubjectGroups = new HashSet<UserSubjectGroup>();
            Subjects = new HashSet<UserSubject>();
            Majors = new HashSet<UserMajor>();
            Colleges = new HashSet<UserColleges>();
            Blogs = new HashSet<User_Blog>();
            Comments = new HashSet<Comment>();
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
        public int? AdminIdUpdate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string Password { get; set; }

        public virtual SysUser UpdateAdmin { get; set; }
        public virtual SysUserRole Role { get; set; }
        public virtual ICollection<TestResults> TestResults { get; set; }
        public virtual ICollection<UserLession> UserLession { get; set; }
        public virtual ICollection<VcGuidance> VcGuidance { get; set; }
        public virtual ICollection<UserSubjectGroup> SubjectGroups { get; set; }
        public virtual ICollection<UserSubject> Subjects { get; set; }
        public virtual ICollection<UserMajor> Majors { get; set; }
        public virtual ICollection<UserColleges> Colleges { get; set; }
        public virtual ICollection<ChatRoom> Students { get; set; }
        public virtual ICollection<ChatRoom> Connectors { get; set; }
        public virtual ICollection<User_Blog > Blogs { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
