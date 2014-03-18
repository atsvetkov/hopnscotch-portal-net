using System;

namespace Hopnscotch.Portal.Contracts
{
    public interface IAttendanceUow : IDisposable
    {
        ILessonRepository Lessons { get; }
        IAttendanceRepository Attendances { get; }

        void Commit();
    }
}
