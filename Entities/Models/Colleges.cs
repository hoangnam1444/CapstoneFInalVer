using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Entities.Models
{
    public partial class Colleges
    {
        public Colleges()
        {
            CollegeRefMajor = new HashSet<CollegeRefMajor>();
            VcGuidance = new HashSet<VcGuidance>();
        }

        public int CollegeId { get; set; }
        public string CollegeName { get; set; }
        public float ToGpa { get; set; }
        public float FromGpa { get; set; }
        public bool? IsDeleted { get; set; }
        public int CollegeTypeId { get; set; }

        public virtual ICollection<CollegeRefMajor> CollegeRefMajor { get; set; }
        public virtual ICollection<VcGuidance> VcGuidance { get; set; }
    }
}
