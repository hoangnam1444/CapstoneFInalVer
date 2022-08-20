using System.ComponentModel.DataAnnotations;

namespace Entities.RequestFeature
{
    public class UpdateTest
    {
        [MaxLength(50)]
        public string TestDescript { get; set; }
        public int TypeId { get; set; }
    }
}
