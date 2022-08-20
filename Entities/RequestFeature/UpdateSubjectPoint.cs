using System.Collections.Generic;

namespace Entities.RequestFeature
{
    public class SubjectSelection
    {
        public List<UpdateSubjectPoint> ListSubject { get; set; }
    }
    public class UpdateSubjectPoint
    {
        public int SubjectId { get; set; }
        public double Point { get; set; }
    }
}
