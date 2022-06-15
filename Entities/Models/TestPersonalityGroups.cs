using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Entities.Models
{
    public partial class TestPersonalityGroups
    {
        public TestPersonalityGroups()
        {
            MajorRefPersonality = new HashSet<MajorRefPersonality>();
            TestAnswers = new HashSet<TestAnswers>();
        }

        public int PersonalityGroupId { get; set; }
        public string PersonalityGroupName { get; set; }
        public int TestTypeId { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual TestTypes TestType { get; set; }
        public virtual ICollection<MajorRefPersonality> MajorRefPersonality { get; set; }
        public virtual ICollection<TestAnswers> TestAnswers { get; set; }
    }
}
