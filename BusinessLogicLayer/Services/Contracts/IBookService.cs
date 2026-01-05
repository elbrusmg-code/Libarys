using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Entities;
namespace BusinessLogicLayer.Services.Contracts
{
    public interface IBookService
    {
        void Add(Book book);
        void Update(Book book);
        void Delete(int id);
        Book GetById(int id);
        List<Book> GetAll();
        List<Book> Search(string keyword);
    }
}
