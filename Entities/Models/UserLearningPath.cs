using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Entities.Models
{
    public partial class UserLearningPath
    {
        public int UserId { get; set; }
        public int LearningPathId { get; set; }

        public virtual LearningPaths LearningPath { get; set; }
        public virtual SysUser User { get; set; }
    }
}
