namespace Entities.Models
{
    public class SubjectGroupSubject
    {
        public int GroupSubjectId { get; set; }
        public int SubjectId { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual SubjectGroup SubjectGroup { get; set; }
    }
}
