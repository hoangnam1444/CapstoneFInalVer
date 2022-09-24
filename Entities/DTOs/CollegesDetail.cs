using System.Collections.Generic;

namespace Entities.DTOs
{
    public class CollegesDetail
    {
        public int CollegesId { get; set; }
        public string Name { get; set; }
        public List<MajorCD> Majors { get; set; }
        public string RefLink { get; set; }
        public string Image { get; set; }
        public string Detail { get; set; }
        public string Address { get; set; }
        public List<Connector> AvaiConnector { get; set; }
    }

    public class MajorCD
    {
        public int MajorId { get; set; }
        public string Name { get; set; }
        public List<SubjectGroup> SubjectGroups { get; set; }
    }
}
