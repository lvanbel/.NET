using System;
using System.Collections.Generic;

namespace SchoolApp.Models
{
    public partial class Professor
    {
        public Professor()
        {
            Courses = new HashSet<Course>();
        }

        public int ProfessorId { get; set; }
        public string Name { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public int SectionId { get; set; }

        public virtual Section Section { get; set; } = null!;
        public virtual ICollection<Course> Courses { get; set; }
    }
}
