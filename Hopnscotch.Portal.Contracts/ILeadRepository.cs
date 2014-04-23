using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Contracts
{
    public interface ILeadRepository : IRepository<Lead>
    {
        Lead GetByAmoId(int amoId);
    }
}