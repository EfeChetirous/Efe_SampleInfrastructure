using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;
using Infrastructure.Entity;

namespace Infrastructure.Data
{
    public class DataContext: DbContext,IDbContext
    {
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => !String.IsNullOrEmpty(type.Namespace))
            .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }

        public virtual TEntity AttachEntityToContext<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            //HACK : little hack here until Entity Framework really supports stored procedures
            //otherwise, navigation properties of loaded entities are not loaded until an entity is attached to the context
            var entry = Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                var set = Set<TEntity>();
                var attachedEntity = set.Find(entity.BaseId);

                if (attachedEntity != null)
                {
                    var attachedEntry = Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    //attach new entity
                    Set<TEntity>().Attach(entity);
                    SetEntityAsModified(entity);
                }
            }

            var alreadyAttached = Find(entity);
            //entity is already loaded.
            return alreadyAttached;
        }

        private TEntity Find<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            var attachedEntity = Set<TEntity>().Local.FirstOrDefault(e => e == entity) ?? Set<TEntity>().Find(entity.BaseId);
            return attachedEntity;
        }
        private void SetEntityAsModified<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            //var entry = Entry(entity);
            //entry.State = EntityState.Modified;
            ChangeTracker.Entries<TEntity>().First(e => e.Entity == entity).State = EntityState.Modified;
        }
    }
}
