namespace Entities.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int ReceiverId { get; set; }
        public string Message { get; set; }
        public bool IsReaded { get; set; }
    }
}
