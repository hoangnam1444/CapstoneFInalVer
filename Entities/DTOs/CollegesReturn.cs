using Entities.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class DashboardColleges
    {
        public string ViewPointLink { get; set; }
        public Pagination<CollegesReturn> College { get; set; }
    }
    public class CollegesReturn
    {
        public int CollegeId { get; set; }
        public string CollegeName { get; set; }
        public string ReferenceLink { get; set; }
        public string Address { get; set; }
        public string ImagePath { get; set; }
        public int NumOfSelected { get; set; }
        public bool IsSelected { get; set; }
        public List<Major> Major { get; set; }
    }
    public class Major
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public List<SubjectGroup> SubjectGroup { get; set; }
    }
    public class SubjectGroup 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double SumPoint { get; set; }
    }

    public class CollegesInList
    {
        public int CollegeId { get; set; }
        public string CollegeName { get; set; }
        public string ReferenceLink { get; set; }
        public string Address { get; set; }
        public string ImagePath { get; set; }
    }
}
