using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Entities.Models
{
    public partial class TestDeclarations
    {
        public TestDeclarations()
        {
            TestQuestions = new HashSet<TestQuestions>();
            TestResults = new HashSet<TestResults>();
        }

        public int TestId { get; set; }
        public string TestDescrip { get; set; }
        public int TestTypeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual TestTypes TestType { get; set; }
        public virtual ICollection<TestQuestions> TestQuestions { get; set; }
        public virtual ICollection<TestResults> TestResults { get; set; }
    }
}
