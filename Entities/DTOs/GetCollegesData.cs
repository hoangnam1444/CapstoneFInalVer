namespace Entities.DTOs
{
    public class GetCollegesData
    {
        public int UserId { get; set; }
        public int SubjectGroupId { get; set; }
        public double Sum { get; set; }
    }

    public class AttempData
    {
        public int MajorId { get; set; }
        public GetCollegesData Data { get; set; }
    }
}
