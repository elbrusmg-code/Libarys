using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class OperationsRepository : IRepository<Operation>
    {
        private static readonly string _path = Path.Combine(Directory.GetCurrentDirectory(), "operations.txt");


        private const int SPACE = 2;

        private const int ID_LENGTH = 5;
        private const int BOOK_ID_LENGTH = 5;
        private const int MEMBER_ID_LENGTH = 5;
        private const int BORROW_DATE_LENGTH = 10;
        private const int RETURN_DATE_LENGTH = 10;
        private const int IS_RETURNED_LENGTH = 1;

        public OperationsRepository()
        {
            string directory = Path.GetDirectoryName(_path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(_path))
            {
                File.Create(_path).Close();
            }
        }

        public void Add(Operation entity)
        {
            var operations = GetAll();
            entity.Id = operations.Any() ? operations.Max(x => x.Id) + 1 : 1;
            entity.BorrowDate = DateTime.Now;
            entity.IsReturned = false;
            entity.ReturnDate = null;

            string line = ConvertLine(entity);
            File.AppendAllText(_path, line + Environment.NewLine);
        }

        public void Delete(int id)
        {
            var operations = GetAll();
            int removeCount = operations.RemoveAll(x => x.Id == id);
            if (removeCount == 0)
            {
                throw new Exception("Əməliyyat tapılmadı!");
            }
            SaveAll(operations);
        }

        public List<Operation> GetAll()
        {
            var operations = new List<Operation>();
            if (!File.Exists(_path))
                return operations;

            foreach (var line in File.ReadAllLines(_path))
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    operations.Add(ParseFromLine(line));
                }
            }
            return operations;
        }

        public Operation GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public List<Operation> Search(string keyword)
        {
            var operations = GetAll();
            if (string.IsNullOrWhiteSpace(keyword))
                return operations;

            keyword = keyword.ToLower();

            return operations.Where(x =>
                x.Id.ToString().Contains(keyword) ||
                x.BookId.ToString().Contains(keyword) ||
                x.MemberId.ToString().Contains(keyword)
            ).ToList();
        }

        public void Uptade(Operation entity)
        {
            var operations = GetAll();
            var index = operations.FindIndex(x => x.Id == entity.Id);
            if (index == -1)
            {
                throw new Exception("Əməliyyat tapılmadı!");
            }
            operations[index] = entity;
            SaveAll(operations);
        }

       
        public List<Operation> GetByMemberId(int memberId)
        {
            return GetAll().Where(x => x.MemberId == memberId).ToList();
        }

        
        public List<Operation> GetByBookId(int bookId)
        {
            return GetAll().Where(x => x.BookId == bookId).ToList();
        }

        
        public List<Operation> GetActiveOperations()
        {
            return GetAll().Where(x => !x.IsReturned).ToList();
        }

        
        public List<Operation> GetActiveMemberOperations(int memberId)
        {
            return GetAll().Where(x => x.MemberId == memberId && !x.IsReturned).ToList();
        }

        private string ConvertLine(Operation operation)
        {
            string id = operation.Id.ToString().PadLeft(ID_LENGTH, '0');
            string bookId = operation.BookId.ToString().PadLeft(BOOK_ID_LENGTH, '0');
            string memberId = operation.MemberId.ToString().PadLeft(MEMBER_ID_LENGTH, '0');
            string borrowDate = operation.BorrowDate.ToString("yyyy-MM-dd");
            string returnDate = operation.ReturnDate?.ToString("yyyy-MM-dd") ?? "0000-00-00";
            string isReturned = operation.IsReturned ? "1" : "0";

            string separator = new string(' ', SPACE);

            return id + separator + bookId + separator + memberId + separator +
                   borrowDate + separator + returnDate + separator + isReturned;
        }

        private Operation ParseFromLine(string line)
        {
            int pos = 0;

            try
            {
                Operation operation = new Operation();

                operation.Id = int.Parse(line.Substring(pos, ID_LENGTH).Trim());
                pos += ID_LENGTH + SPACE;

                operation.BookId = int.Parse(line.Substring(pos, BOOK_ID_LENGTH).Trim());
                pos += BOOK_ID_LENGTH + SPACE;

                operation.MemberId = int.Parse(line.Substring(pos, MEMBER_ID_LENGTH).Trim());
                pos += MEMBER_ID_LENGTH + SPACE;

                string borrowDateStr = line.Substring(pos, BORROW_DATE_LENGTH).Trim();
                operation.BorrowDate = DateTime.ParseExact(borrowDateStr, "yyyy-MM-dd", null);
                pos += BORROW_DATE_LENGTH + SPACE;

                string returnDateStr = line.Substring(pos, RETURN_DATE_LENGTH).Trim();
                if (returnDateStr != "0000-00-00")
                {
                    operation.ReturnDate = DateTime.ParseExact(returnDateStr, "yyyy-MM-dd", null);
                }
                pos += RETURN_DATE_LENGTH + SPACE;

                operation.IsReturned = line.Substring(pos, IS_RETURNED_LENGTH) == "1";

                return operation;
            }
            catch (Exception ex)
            {
                throw new Exception($"Fayl oxunarkən xəta: {ex.Message}");
            }
        }

        private void SaveAll(List<Operation> operations)
        {
            var lines = operations.Select(t => ConvertLine(t)).ToList();
            File.WriteAllLines(_path, lines);
        }
    }
}
