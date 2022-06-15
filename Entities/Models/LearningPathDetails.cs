using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Entities.Models
{
    public partial class LearningPathDetails
    {
        public int LearningPathDetailId { get; set; }
        public int LearningPathId { get; set; }
        public string LearningPathDetailContent { get; set; }
        public int OrderIndex { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual LearningPaths LearningPath { get; set; }
    }
}
