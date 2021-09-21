using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T>, IDisposable where T : class
    {
        private DbSet<T> _entities;
        private string _errorMessage = string.Empty;
        private bool _isDisposed;

        public RepositoryBase(ApplicationDbContext context)
        {
            _isDisposed = false;
            Context = context;

        }
        public ApplicationDbContext Context { get; set; }
        public virtual IQueryable<T> Table
        {
            get { return Entities; }
        }
        protected virtual DbSet<T> Entities
        {
            get { return _entities ?? (_entities = Context.Set<T>()); }
        }
        public void Dispose()
        {
            if (Context != null)
                Context.Dispose();
            _isDisposed = true;
        }
        public virtual IEnumerable<T> GetAll()
        {
            return Entities.ToList();
        }
        public virtual T GetById(object id)
        {
            return Entities.Find(id);
        }
        public virtual void Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");
                Entities.Add(entity);
                //if (Context == null || _isDisposed)
                //    Context = (ApplicationDbContext)ServiceProvider.GetService(typeof(ApplicationDbContext));
                ////Context.SaveChanges(); commented out call to SaveChanges as Context save changes will be 
                ////called with Unit of work
            }
            catch (Exception dbEx)
            {
                throw new Exception(_errorMessage, dbEx);
            }
        }
        public void BulkInsert(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                {
                    throw new ArgumentNullException("entities");
                }
                //Context.Configuration.AutoDetectChangesEnabled = false;
                Context.Set<T>().AddRange(entities);
                Context.SaveChanges();
            }
            catch (Exception dbEx)
            {

                throw new Exception(_errorMessage, dbEx);
            }
        }
        public virtual void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");
                //if (Context == null || _isDisposed)
                //    Context = (ApplicationDbContext)_serviceProvider.GetService(typeof(ApplicationDbContext));
                SetEntryModified(entity);
                //Context.SaveChanges(); commented out call to SaveChanges as Context save changes will be called with Unit of work
            }
            catch (Exception dbEx)
            {

                throw new Exception(_errorMessage, dbEx);
            }
        }
        public virtual void Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");
                //if (Context == null || _isDisposed)
                //    Context = (ApplicationDbContext)_serviceProvider.GetService(typeof(ApplicationDbContext));
                Entities.Remove(entity);
                //Context.SaveChanges(); commented out call to SaveChanges as Context save changes will be called with Unit of work
            }
            catch (Exception dbEx)
            {

                throw new Exception(_errorMessage, dbEx);
            }
        }
        public virtual void SetEntryModified(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            return Entities.AsNoTracking().Where(predicate);
        }
    }
}
