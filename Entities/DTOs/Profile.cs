using System;

namespace Entities.DTOs
{
    public class Profile
    {
        public string FullName { get; set; }
        public bool? Gender { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string ImagePath { get; set; }
    }
}
