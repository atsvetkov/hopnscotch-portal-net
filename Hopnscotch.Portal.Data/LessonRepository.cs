using System.Data.Entity;
using Hopnscotch.Portal.Contracts;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Data
{
    public sealed class LessonRepository : EFRepository<Lesson>, ILessonRepository
    {
        public LessonRepository(DbContext context) : base(context)
        {
        }
    }
}