using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Services.Contracts
{
    public interface ICategoryService
    {
        void Add(Category category);
        void Update(Category category);
        void Delete(int id);
        Category GetById(int id);
        List<Category> GetAll();
        List<Category> Search(string keyword);
    }
}
