using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class AnswerDetail
    {
        public AnswerOfQuestion Answer { get; set; }
        public List<PGroupOfAnswer> PeronalityGroups { get; set; }
    }
}
