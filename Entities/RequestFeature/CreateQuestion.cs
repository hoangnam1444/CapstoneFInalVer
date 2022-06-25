using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.RequestFeature
{
    public class CreateQuestion
    {
        [Required]
        [MaxLength(500)]
        public string QuestionContent { get; set; }
        public int OrderIndex { get; set; }
    }
}
