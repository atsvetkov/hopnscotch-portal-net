using System;
using System.Data.Entity;

namespace Hopnscotch.Portal.Data.Helpers
{
    internal interface IRepositoryFactories
    {
        Func<DbContext, object> GetRepositoryFactory<T>();
        Func<DbContext, object> GetRepositoryFactoryForEntityType<T>() where T : class;
    }
}