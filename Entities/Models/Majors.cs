using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Entities.Models
{
    public partial class Majors
    {
        public Majors()
        {
            CollegeRefMajor = new HashSet<CollegeRefMajor>();
            LearningPaths = new HashSet<LearningPaths>();
            MajorRefPersonality = new HashSet<MajorRefPersonality>();
            VcGuidance = new HashSet<VcGuidance>();
        }

        public int MajorId { get; set; }
        public string MajorName { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<CollegeRefMajor> CollegeRefMajor { get; set; }
        public virtual ICollection<LearningPaths> LearningPaths { get; set; }
        public virtual ICollection<MajorRefPersonality> MajorRefPersonality { get; set; }
        public virtual ICollection<VcGuidance> VcGuidance { get; set; }
    }
}
