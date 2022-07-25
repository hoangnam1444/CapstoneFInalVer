using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class MajorResult
    {
        public int MajorId { get; set; }
        public string MajorName { get; set; }
        public string Description { get; set; } 
    }
    public class MajorForFilter
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
