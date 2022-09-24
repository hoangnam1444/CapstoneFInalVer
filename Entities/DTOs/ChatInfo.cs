using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class ChatInfo
    {
        public int UserId { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
