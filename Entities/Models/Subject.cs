using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class Subject
    {
        public Subject()
        {
            SubjectGroups = new HashSet<SubjectGroupSubject>();
            Users = new HashSet<UserSubject>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<SubjectGroupSubject> SubjectGroups { get; set; }
        public virtual ICollection<UserSubject> Users { get; set; }
    }
}
