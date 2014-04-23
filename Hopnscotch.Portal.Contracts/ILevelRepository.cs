using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Contracts
{
    public interface ILevelRepository : IRepository<Level>
    {
        Level GetByAmoId(int amoId);
    }
}