using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class PGroupStatistic
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public double AvgPoint { get; set; }
        public string Description { get; set; }
    }
}
