using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.RequestFeature
{
    public class UpdateTest
    {
        [MaxLength(50)]
        public string TestDescript { get; set; }
        public int TypeId { get; set; }
    }
}
