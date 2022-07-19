using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class TestDetail
    {
        public int TestId { get; set; }
        public string TestDescrip { get; set; }
        public TestType TestType { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class TestType 
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }
    }
}
