using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Contracts
{
    public interface ILeadStatusRepository : IRepository<LeadStatus>
    {
        LeadStatus GetByAmoId(int amoId);
    }
}