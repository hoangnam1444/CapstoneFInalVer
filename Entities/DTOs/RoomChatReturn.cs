using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class RoomChatReturn
    {
        public int RoomId { get; set; }
        public string CollegeName { get; set; }
        public string ConnectorName { get; set; }
        public string ConnectorAvatar { get; set; }
    }
}
