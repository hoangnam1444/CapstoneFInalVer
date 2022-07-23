using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Entities.Models
{
    public partial class LessionDetails
    {
        public int LessionDetailId { get; set; }
        public int LessionId { get; set; }
        public string LessionDetailContent { get; set; }
        public string Link { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual RecommentLession Lession { get; set; }
    }
}
