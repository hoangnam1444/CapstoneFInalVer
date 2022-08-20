using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Entities.Models
{
    public partial class TestQuestions
    {
        public TestQuestions()
        {
            TestAnswers = new HashSet<TestAnswers>();
            TestResults = new HashSet<TestResults>();
        }

        public int QuestionId { get; set; }
        public int TestId { get; set; }
        public string QuestionContent { get; set; }
        public int OrderIndex { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual TestDeclarations Test { get; set; }
        public virtual ICollection<TestAnswers> TestAnswers { get; set; }
        public virtual ICollection<TestResults> TestResults { get; set; }
    }
}
