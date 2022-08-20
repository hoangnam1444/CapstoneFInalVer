using System.Collections.Generic;

namespace Entities.DTOs
{
    public class AnswerDetail
    {
        public AnswerOfQuestion Answer { get; set; }
        public List<PGroupOfAnswer> PeronalityGroups { get; set; }
    }
}
