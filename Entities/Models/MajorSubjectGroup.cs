using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class MajorSubjectGroup
    {
        public int MajorId { get; set; }
        public int SubjectGroupId { get; set; }

        public virtual Majors Major { get; set; }
        public virtual SubjectGroup SubjectGroup { get; set; }
    }
}
