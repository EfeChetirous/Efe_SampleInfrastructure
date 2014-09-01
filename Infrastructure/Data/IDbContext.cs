using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Infrastructure.Entity;

namespace Infrastructure.Data
{
    public interface IDbContext
    {

        IDbSet<TEntity> Set<TEntity>() where TEntity:class;
        int SaveChanges();
        void Dispose();
        TEntity AttachEntityToContext<TEntity>(TEntity entity) where TEntity : BaseEntity;
    }
}
