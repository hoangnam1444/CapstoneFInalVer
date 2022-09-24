using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class Blog
    {
        public Blog()
        {
            Users = new HashSet<User_Blog>();
            Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<User_Blog> Users { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
