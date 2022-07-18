using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.RequestFeature
{
    public class NewAnswer
    {
        [Required]
        [MaxLength(200)]
        public string AnswerContent { get; set; }
        public List<int> PersonalityGroupId { get; set; }
        public int Point { get; set; }
        public int OrderIndex { get; set; }
    }
}
