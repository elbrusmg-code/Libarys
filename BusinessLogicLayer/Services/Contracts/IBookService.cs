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
        void BorrowBook(int bookId, int memberId);
        void ReturnBook(int bookId);
        List<Operation> GetActiveOperations();
        List<Operation> GetOperationHistory(int? bookId = null, int? memberId = null);
        void DeactivateMemberAndReturnBooks(int memberId);
    }
}
