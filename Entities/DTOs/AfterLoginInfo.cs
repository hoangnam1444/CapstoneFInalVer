namespace Entities.DTOs
{
    public class AfterLoginInfo
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Avatar { get; set; }
        public string Token { get; set; }
        public bool HasGrade { get; set; }
        public bool? IsActive { get; set; }
        public int RoleId { get; set; }
    }
}
