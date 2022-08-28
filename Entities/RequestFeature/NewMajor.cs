using System.Collections.Generic;

namespace Entities.RequestFeature
{
    public class NewMajor
    {
        public string Name { get; set; }
        public List<int> SubjectGroupIds { get; set; }
    }
}
