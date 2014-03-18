using System;
using System.Collections.Generic;
using System.Data.Entity;
using Hopnscotch.Portal.Contracts;

namespace Hopnscotch.Portal.Data.Helpers
{
    internal sealed class RepositoryProvider : IRepositoryProvider
    {
        public RepositoryProvider(IRepositoryFactories repositoryFactories)
        {
            this.repositoryFactories = repositoryFactories;
            Repositories = new Dictionary<Type, object>();
        }

        public DbContext DbContext { get; set; }

        public IRepository<T> GetRepositoryForEntityType<T>() where T : class
        {
            return GetRepository<IRepository<T>>(repositoryFactories.GetRepositoryFactoryForEntityType<T>());
        }

        public T GetRepository<T>(Func<DbContext, object> factory = null) where T : class
        {
            object repoObj;
            Repositories.TryGetValue(typeof(T), out repoObj);
            if (repoObj != null)
            {
                return (T)repoObj;
            }

            return MakeRepository<T>(factory, DbContext);
        }

        private Dictionary<Type, object> Repositories { get; set; }

        private T MakeRepository<T>(Func<DbContext, object> factory, DbContext dbContext)
        {
            var f = factory ?? repositoryFactories.GetRepositoryFactory<T>();
            if (f == null)
            {
                throw new NotImplementedException("No factory for repository type, " + typeof(T).FullName);
            }
            var repo = (T)f(dbContext);
            Repositories[typeof(T)] = repo;
            return repo;
        }

        public void SetRepository<T>(T repository)
        {
            Repositories[typeof(T)] = repository;
        }

        private readonly IRepositoryFactories repositoryFactories;
    }
}
