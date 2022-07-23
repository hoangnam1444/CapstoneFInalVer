using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Entities.Models
{
    public partial class RecommentLession
    {
        public RecommentLession()
        {
            UserLession = new HashSet<UserLession>();
        }

        public int LessionId { get; set; }
        public int MajorId { get; set; }
        public string Description { get; set; }
        public bool? IsDeleted { get; set; }
        public int LessionDetailId { get; set; }

        public virtual Majors Major { get; set; }
        public virtual LessionDetails LessionDetail { get; set; }
        public virtual ICollection<UserLession> UserLession { get; set; }
    }
}
