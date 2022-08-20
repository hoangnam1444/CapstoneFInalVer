using System.Collections.Generic;

namespace Entities.DTOs
{
    public class SubjectGroupReturn
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SubjectReturn> Subjects { get; set; }
    }
}
