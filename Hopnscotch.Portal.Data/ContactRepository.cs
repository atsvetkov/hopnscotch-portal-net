using System.Data.Entity;
using System.Linq;
using Hopnscotch.Portal.Contracts;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Data
{
    public sealed class ContactRepository : EFRepository<Contact>, IContactRepository
    {
        public ContactRepository(DbContext context)
            : base(context)
        {
        }

        public Contact GetByAmoId(int amoId)
        {
            return Entities.FirstOrDefault(l => l.AmoId == amoId);
        }
    }
}