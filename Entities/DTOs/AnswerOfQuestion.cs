namespace Entities.DTOs
{
    public class AnswerOfQuestion
    {
        public int AnswerId { get; set; }
        public string AnswerContent { get; set; }
        public int PersonalityGroupId { get; set; }
        public int Point { get; set; }
        public int OrderIndex { get; set; }
    }
}
