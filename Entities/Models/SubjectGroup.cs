using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class SubjectGroup
    {
        public SubjectGroup()
        {
            Subjects = new HashSet<SubjectGroupSubject>();
            Majors = new HashSet<MajorSubjectGroup>();
            Users = new HashSet<UserSubjectGroup>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SubjectGroupSubject> Subjects { get; set; }
        public virtual ICollection<MajorSubjectGroup> Majors { get; set; }
        public virtual ICollection<UserSubjectGroup> Users { get; set; }
    }
}
