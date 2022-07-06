using System.ComponentModel.DataAnnotations;

namespace Entities.RequestFeature
{
    public class UpdateAnswer
    {
        [MaxLength(200)]
        public string AnswerContent { get; set; }
        public int QuestionId { get; set; }
        public int OrderIndex { get; set; }
    }
}
