// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Entities.Models
{
    public partial class CollegeRefMajor
    {
        public int MajorId { get; set; }
        public int CollegeId { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Colleges College { get; set; }
        public virtual Majors Major { get; set; }
    }
}
