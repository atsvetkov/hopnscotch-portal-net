using System;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Contracts
{
    public interface IAttendanceUow : IDisposable
    {
        IRepository<Lesson> Lessons { get; }
        IRepository<Attendance> Attendances { get; }
        IImportDataRepository ImportData { get; }
        IContactRepository Contacts { get; }
        ILeadRepository Leads { get; }
        ITaskRepository Tasks { get; }
        IUserRepository Users { get; }
        ILevelRepository Levels { get; }
        ILeadStatusRepository LeadStatuses { get; }

        void Commit();
    }
}
