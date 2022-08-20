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
        public bool? IsDeleted { get; set; }
        public string LessionDetailContent { get; set; }
        public string Link { get; set; }
        public string ImageUrl { get; set; }

        public virtual Majors Major { get; set; }
        public virtual ICollection<UserLession> UserLession { get; set; }
    }
}
