using System.Data.Entity;
using System.Linq;
using Hopnscotch.Portal.Contracts;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Data
{
    public sealed class LeadStatusRepository : EFRepository<LeadStatus>, ILeadStatusRepository
    {
        public LeadStatusRepository(DbContext context)
            : base(context)
        {
        }

        public LeadStatus GetByAmoId(int amoId)
        {
            return Entities.FirstOrDefault(l => l.AmoId == amoId);
        }
    }
}