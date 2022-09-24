using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class BlogDetail
    {
        public int BlogId { get; set; }
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string OwnerAvatar { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string BlogImage { get; set; }
        public int NumOfReact { get; set; }
        public int NumOfComment { get; set; }
        public bool IsReacted { get; set; }
    }
}
