using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeature
{
    public class UpdateGradeAndGpa
    {
        public int Grade { get; set; }
        public float? GPA10 { get; set; }
        public float? GPA11 { get; set; }
        public float? GPA12 { get; set; }
    }
}
