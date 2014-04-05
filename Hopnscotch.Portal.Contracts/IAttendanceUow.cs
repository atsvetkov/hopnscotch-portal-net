using System;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Contracts
{
    public interface IAttendanceUow : IDisposable
    {
        IRepository<Lesson> Lessons { get; }
        IRepository<Attendance> Attendances { get; }
        IRepository<Contact> Contacts { get; }
        IRepository<Lead> Leads { get; }
        IRepository<Task> Tasks { get; }
        IRepository<User> Users { get; }
        IRepository<Level> Levels { get; }

        void Commit();
    }
}
