using System;
using System.Collections.Generic;

namespace SchoolApp.Models
{
    public partial class Section
    {
        public Section()
        {
            Professors = new HashSet<Professor>();
            Students = new HashSet<Student>();
        }

        public int SectionId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Professor> Professors { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
