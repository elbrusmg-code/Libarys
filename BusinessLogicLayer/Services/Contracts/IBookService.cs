using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogicLayer.Dtos;
using DataAccessLayer.Entities;
namespace BusinessLogicLayer.Services.Contracts
{
    public interface IBookService
    {
        void Add(BookCteateDto bookDto);
        void Update(BookUptadeDto bookUp);
        void Delete(int id);
        Book GetById(int id);
        List<Book> GetAll();
        List<Book> Search(string keyword);
    }
}
