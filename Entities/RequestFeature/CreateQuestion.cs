using System.ComponentModel.DataAnnotations;

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
