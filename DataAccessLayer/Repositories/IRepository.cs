using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Add(T  entity);
        T GetById(int id);
        List<T> GetAll();
        void Uptade(T entity);
        void Delete (int id);
        List<T> Search(string keyword);
    }
}
