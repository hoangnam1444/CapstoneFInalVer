using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeature
{
    public class UpdateProfileInfo
    {
        public string FullName { get; set; }
        public bool? Gender { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? BirthDay { get; set; }
        public string UserName { get; set; }
        public string ImagePath { get; set; }
        public int? Grade { get; set; }
    }
}
