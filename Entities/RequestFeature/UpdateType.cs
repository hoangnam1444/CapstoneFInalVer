using System.ComponentModel.DataAnnotations;

namespace Entities.RequestFeature
{
    public class UpdateType
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
