using System.ComponentModel.DataAnnotations;

namespace Entities.RequestFeature
{
    public class UpdatePGroup
    {
        [MaxLength(50)]
        public string PersonalityGroupName { get; set; }
        public int TestTypeId { get; set; }
    }
}
