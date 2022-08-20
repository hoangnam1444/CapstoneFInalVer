// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Entities.Models
{
    public partial class MajorRefPersonality
    {
        public int PersonalityGroupId { get; set; }
        public int MajorId { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Majors Major { get; set; }
        public virtual TestPersonalityGroups PersonalityGroup { get; set; }
    }
}
