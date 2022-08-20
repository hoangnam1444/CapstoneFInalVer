namespace Entities.Models
{
    public class UserSubjectGroup
    {
        public int UserId { get; set; }
        public int SubjectGroupId { get; set; }

        public virtual SysUser User { get; set; }
        public virtual SubjectGroup SubjectGroup { get; set; }
    }
}
