using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Entities.Models
{
    public partial class TestTypes
    {
        public TestTypes()
        {
            TestDeclarations = new HashSet<TestDeclarations>();
            TestPersonalityGroups = new HashSet<TestPersonalityGroups>();
        }

        public int TestTypeId { get; set; }
        public string TestTypeName { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<TestDeclarations> TestDeclarations { get; set; }
        public virtual ICollection<TestPersonalityGroups> TestPersonalityGroups { get; set; }
    }
}
