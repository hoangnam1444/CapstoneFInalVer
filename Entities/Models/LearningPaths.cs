using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Entities.Models
{
    public partial class LearningPaths
    {
        public LearningPaths()
        {
            LearningPathDetails = new HashSet<LearningPathDetails>();
            UserLearningPath = new HashSet<UserLearningPath>();
        }

        public int LearningPathId { get; set; }
        public int MajorId { get; set; }
        public string Description { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Majors Major { get; set; }
        public virtual ICollection<LearningPathDetails> LearningPathDetails { get; set; }
        public virtual ICollection<UserLearningPath> UserLearningPath { get; set; }
    }
}
