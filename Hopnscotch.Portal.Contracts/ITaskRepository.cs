using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Contracts
{
    public interface ITaskRepository : IRepository<Task>
    {
        Task GetByAmoId(int amoId);
    }
}