using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Contracts
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByAmoId(int amoId);
    }
}