namespace Entities.Models
{
    public class UserSubject
    {
        public int UserId { get; set; }
        public int SubjectId { get; set; }
        public double Point { get; set; }

        public virtual SysUser User { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
