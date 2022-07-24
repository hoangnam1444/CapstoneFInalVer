using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeature
{
    public class UpdateCollege
    {
        public int CollegeId { get; set; }
        public string CollegeName { get; set; }
        public string ReferenceLink { get; set; }
        public string Address { get; set; }
        public string ImagePath { get; set; }
    }
}
