using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Entity;
using Infrastructure.Entity.Models;

namespace Infrastructure.Service
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        Category GetById(int id);
        void Insert(Category model);
        void Update(Category model);
        void Delete(Category model);

    }
}
