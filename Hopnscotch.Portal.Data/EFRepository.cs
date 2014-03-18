using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Hopnscotch.Portal.Contracts;

namespace Hopnscotch.Portal.Data
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        public EFRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            Context = context;
            Entities = Context.Set<T>();
        }

        protected DbSet<T> Entities { get; set; }

        private DbContext Context  { get; set; }

        public virtual IQueryable<T> GetAll()
        {
            return Entities;
        }

        public virtual T GetById(int id)
        {
            return Entities.Find(id);
        }

        public virtual void Add(T entity)
        {
            DbEntityEntry entry = Context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                Entities.Add(entity);
            }
        }

        public virtual void Update(T entity)
        {
            DbEntityEntry entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                Entities.Attach(entity);
            }
            
            entry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            DbEntityEntry entry = Context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                Entities.Attach(entity);
                Entities.Remove(entity);
            }
        }

        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null)
            {
                return;
            }

            Delete(entity);
        }
    }
}