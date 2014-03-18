namespace Hopnscotch.Portal.Model
{
    public class Attendance
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public int ClientId { get; set; }
        public bool Attended { get; set; }

        public virtual Lesson Lesson { get; set; }
    }
}