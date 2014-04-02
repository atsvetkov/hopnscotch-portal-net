namespace Hopnscotch.Portal.Model
{
    public class Attendance : EntityBase
    {
        public bool Attended { get; set; }

        public int LessonId { get; set; }
        public int ContactId { get; set; }

        public virtual Lesson Lesson { get; set; }
        public virtual Contact Contact { get; set; }
    }
}