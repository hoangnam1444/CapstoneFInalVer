using System.ComponentModel.DataAnnotations;

namespace Entities.RequestFeature
{
    public class CreateTest
    {
        [Required]
        public string TestDescrip { get; set; }
    }
}
