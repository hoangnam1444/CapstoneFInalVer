using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class AnswersPGroups
    {
        public int Id { get; set; }
        public int PGroupId { get; set; }
        public int AnswerId { get; set; }
        public int Point { get; set; }

        public virtual TestPersonalityGroups PGroup { get; set; }
        public virtual TestAnswers Answer { get; set; }
    }
}
