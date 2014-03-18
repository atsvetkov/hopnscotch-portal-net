using System;
using System.Collections.Generic;
using System.Data.Entity;
using Hopnscotch.Portal.Contracts;

namespace Hopnscotch.Portal.Data.Helpers
{
    internal sealed class RepositoryFactories : IRepositoryFactories
    {
        private IDictionary<Type, Func<DbContext, object>> GetFactories()
        {
            return new Dictionary<Type, Func<DbContext, object>>
                {
                   { typeof(IAttendanceRepository), dbContext => new AttendanceRepository(dbContext) },
                   { typeof(ILessonRepository), dbContext => new LessonRepository(dbContext) }
                };
        }

        public RepositoryFactories()  
        {
            _repositoryFactories = GetFactories();
        }

        public RepositoryFactories(IDictionary<Type, Func<DbContext, object>> factories )
        {
            _repositoryFactories = factories;
        }

        public Func<DbContext, object> GetRepositoryFactory<T>()
        {
            Func<DbContext, object> factory;
            _repositoryFactories.TryGetValue(typeof(T), out factory);
            return factory;
        }

        public Func<DbContext, object> GetRepositoryFactoryForEntityType<T>() where T : class
        {
            return GetRepositoryFactory<T>();
        }

        private readonly IDictionary<Type, Func<DbContext, object>> _repositoryFactories;
    }
}
