using System.Data.Entity;
using System.Linq;
using Hopnscotch.Portal.Contracts;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Data
{
    public sealed class LevelRepository : EFRepository<Level>, ILevelRepository
    {
        public LevelRepository(DbContext context)
            : base(context)
        {
        }

        public Level GetByAmoId(int amoId)
        {
            return Entities.FirstOrDefault(l => l.AmoId == amoId);
        }
    }
}