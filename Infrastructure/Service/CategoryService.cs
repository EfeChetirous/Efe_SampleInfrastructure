using Infrastructure.Entity;
using Infrastructure.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Service
{
    public class CategoryService : ServiceBase, ICategoryService
    {
        private IRepository<Category> categroyRepository;

        public CategoryService(IRepository<Category> categroyRepository)
        {
            this.categroyRepository = categroyRepository;
        }

        public List<Category> GetAll()
        {
            return categroyRepository.GetAll().ToList();
        }

        public Category GetById(int id)
        {
            if (id == 0)
                return null;
            return categroyRepository.GetById(id);
        }

        public void Insert(Category model)
        {
            ExecuteExceptionHandledOperation(() =>
            {
                if (model == null)
                    throw new ArgumentNullException("category");
                categroyRepository.Insert(model);
            });
        }

        public void Update(Category model)
        {
            ExecuteExceptionHandledOperation(() =>
            {
                if (model == null)
                    throw new ArgumentNullException("category");
                categroyRepository.Update(model);
            });
        }

        public void Delete(Category model)
        {
            if (model == null)
                throw new ArgumentNullException("category");
            categroyRepository.Delete(model);
        }
    }
}