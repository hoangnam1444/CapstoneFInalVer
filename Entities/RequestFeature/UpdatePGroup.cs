using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.RequestFeature
{
    public class UpdatePGroup
    {
        [MaxLength(50)]
        public string PersonalityGroupName { get; set; }
        public int TestTypeId { get; set; }
    }
}
