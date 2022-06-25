using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.RequestFeature
{
    public class CreateTest
    {
        [Required]
        [MaxLength(50)]
        public string TestDescrip { get; set; }
    }
}
