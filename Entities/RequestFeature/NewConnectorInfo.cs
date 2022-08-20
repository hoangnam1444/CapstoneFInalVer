using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeature
{
    public class NewConnectorInfo
    {
        public string FullName { get; set; }
        public bool? Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int CollegesId { get; set; }
    }
}
