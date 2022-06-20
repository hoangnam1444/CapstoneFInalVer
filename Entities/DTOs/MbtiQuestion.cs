using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class MbtiQuestion
    {
        public int QuestionId { get; set; }
        public string Content { get; set; }
        public List<AnswerInTest> Answer { get; set; }
        public int Index { get; set; }
    }
}
