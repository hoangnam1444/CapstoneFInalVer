using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class ChatRoom
    {

        public int Id { get; set; }
        public int CollegeId { get; set; }
        public int StudentId { get; set; }
        public int ConnectorId { get; set; }

        public virtual SysUser Student { get; set; }
        public virtual SysUser Connector { get; set; }
    }
}
