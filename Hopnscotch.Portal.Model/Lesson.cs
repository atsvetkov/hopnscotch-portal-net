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
        public LessonStatus Status { get; set; }
        public string Comment { get; set; }
        public bool Finalized { get; set; }

        public virtual Lead Lead { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
    }

    public enum LessonStatus
    {
        None,
        Planned,
        Completed,
        Cancelled
    }
}
