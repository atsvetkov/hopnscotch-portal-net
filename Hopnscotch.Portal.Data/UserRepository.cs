using System.Data.Entity;
using System.Linq;
using Hopnscotch.Portal.Contracts;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Data
{
    public sealed class UserRepository : EFRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context)
            : base(context)
        {
        }

        public User GetByAmoId(int amoId)
        {
            return Entities.FirstOrDefault(l => l.AmoId == amoId);
        }
    }
}