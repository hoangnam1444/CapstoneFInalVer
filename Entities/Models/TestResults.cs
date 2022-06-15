using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Entities.Models
{
    public partial class TestResults
    {
        public int TestResultId { get; set; }
        public int UserId { get; set; }
        public int TestId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public bool? IsLast { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual TestAnswers Answer { get; set; }
        public virtual TestQuestions Question { get; set; }
        public virtual TestDeclarations Test { get; set; }
        public virtual SysUser User { get; set; }
    }
}
