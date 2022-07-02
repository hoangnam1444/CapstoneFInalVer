using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class HollandQuestion
    {
        public int Id { get; set; }
        public string content { get; set; }
        public int Indext { get; set; }
        public int MyProperty { get; set; }
        public List<AnswerInTest> Answers { get; set; }
    }
}
