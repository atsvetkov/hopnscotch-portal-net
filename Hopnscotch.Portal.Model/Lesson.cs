using System;
using System.Collections.Generic;

namespace Hopnscotch.Portal.Model
{
    public class Lesson
    {
        public int Id { get; set; }
        public int LeadId { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<Attendance> AttendanceList { get; set; }
    }
}
