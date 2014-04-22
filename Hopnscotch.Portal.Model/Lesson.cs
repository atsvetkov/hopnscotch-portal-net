using System;
using System.Collections.Generic;

namespace Hopnscotch.Portal.Model
{
    public class Lesson : EntityBase
    {
        public Lesson()
        {
            Attendances = new List<Attendance>();
        }

        public int LeadId { get; set; }
        public DateTime Date { get; set; }
        public int AcademicHours { get; set; }
        public bool Completed { get; set; }

        public virtual Lead Lead { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}
