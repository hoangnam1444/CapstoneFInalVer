using System;

namespace Entities.DTOs
{
    public class UserDetail
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public bool? Gender { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? BirthDay { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public string ImagePath { get; set; }
        public bool? IsLocked { get; set; }
        public int? Grade { get; set; }
        public float? Gpa10 { get; set; }
        public float? Gpa11 { get; set; }
        public float? Gpa12 { get; set; }
        public AdminUpdateInfo AdminUpdate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
