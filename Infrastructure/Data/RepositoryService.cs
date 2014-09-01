using Infrastructure.Entity;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace Infrastructure.Data
{
    public class RepositoryService<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private IDbContext Context;

        private IDbSet<TEntity> Entities
        {
            get { return this.Context.Set<TEntity>(); }
        }

        public RepositoryService(IDbContext context)
        {
            this.Context = context;
        }

        public IQueryable<TEntity> GetAll()
        {
            return Entities.AsQueryable();
        }

        public TEntity GetById(object id)
        {
            return Entities.Find(id);
        }

        public void Insert(TEntity entity)
        {
            Entities.Add(entity);
            this.Context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this.Context.AttachEntityToContext(entity);
                this.Context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors).Aggregate(string.Empty, (current, validationError) => current + (Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage)));
                var fail = new Exception(msg, dbEx);
                throw fail;
            }

            //if (entity == null)
            //    throw new ArgumentNullException("entity");

            //this.Context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                var attachedEntity = this.Context.AttachEntityToContext(entity);
                Entities.Remove(attachedEntity);
                this.Context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors)
                                                     .Aggregate(string.Empty, (current, validationError) => current + (Environment.NewLine + string.Format("Property: {0} Error: {1}"
                                                         , validationError.PropertyName
                                                         , validationError.ErrorMessage)));

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.Context != null)
                {
                    this.Context.Dispose();
                    this.Context = null;
                }
            }
        }
    }
}