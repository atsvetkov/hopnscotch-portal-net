using System.Data.Entity;
using System.Linq;
using Hopnscotch.Portal.Contracts;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Data
{
    public sealed class TaskRepository : EFRepository<Task>, ITaskRepository
    {
        public TaskRepository(DbContext context)
            : base(context)
        {
        }

        public Task GetByAmoId(int amoId)
        {
            return Entities.FirstOrDefault(l => l.AmoId == amoId);
        }
    }
}