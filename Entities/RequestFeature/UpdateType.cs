using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.RequestFeature
{
    public class UpdateType
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
