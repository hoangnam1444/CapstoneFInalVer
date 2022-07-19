using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class CollegesReturn
    {
        public int CollegeId { get; set; }
        public string CollegeName { get; set; }
        public string ReferenceLink { get; set; }
        public string Address { get; set; }
        public string ImagePath { get; set; }
        public Major Major { get; set; }
        public SubjectGroup SubjectGroup { get; set; }
    }
    public class Major
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
    public class SubjectGroup 
    {
        public string Name { get; set; }
        public double SumPoint { get; set; }
    }
}
