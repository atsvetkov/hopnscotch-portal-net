using System.Data.Entity;

namespace Hopnscotch.Portal.Data
{
    public interface IContextFactory<out T> where T : DbContext
    {
        T GetContext();
    }
}