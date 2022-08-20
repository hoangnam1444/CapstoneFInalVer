namespace Entities.Models
{
    public class SecurityCode
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Code { get; set; }

        public virtual SysUser user { get; set; }
    }
}
