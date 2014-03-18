using System;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Contracts
{
    public interface IAttendanceUow : IDisposable
    {
        IRepository<Lesson> Lessons { get; }
        IRepository<Attendance> Attendances { get; }

        void Commit();
    }
}
