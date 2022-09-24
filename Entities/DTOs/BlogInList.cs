using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class BlogInList
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int NumOfReact { get; set; }
        public int NumOfComment { get; set; }
        public bool IsReacted { get; set; }
    }
}
