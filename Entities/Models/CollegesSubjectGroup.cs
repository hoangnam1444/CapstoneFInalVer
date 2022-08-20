namespace Entities.Models
{
    public class CollegesSubjectGroup
    {
        public int CollegesId { get; set; }
        public int SubjectGroupId { get; set; }
        public int MajorId { get; set; }
        public double Sum { get; set; }

        public virtual Colleges College { get; set; }
        public virtual SubjectGroup SubjectGroup { get; set; }
        public virtual Majors Major { get; set; }
    }
}
