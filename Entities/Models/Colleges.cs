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
            SubjectGroups = new HashSet<CollegesSubjectGroup>();
        }

        public int CollegeId { get; set; }
        public string CollegeName { get; set; }
        public string ReferenceLink { get; set; }
        public string Address { get; set; }
        public string Detail { get; set; }
        public string ImagePath { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<CollegeRefMajor> CollegeRefMajor { get; set; }
        public virtual ICollection<VcGuidance> VcGuidance { get; set; }
        public virtual ICollection<CollegesSubjectGroup> SubjectGroups { get; set; }
        public virtual ICollection<UserColleges> Users { get; set; }
    }
}
