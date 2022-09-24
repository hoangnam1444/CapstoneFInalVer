using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BlogId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }

        public SysUser User { get; set; }
        public Blog Blog { get; set; }
    }
}
