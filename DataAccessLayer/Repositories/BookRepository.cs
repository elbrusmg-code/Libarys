using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class BookRepository : IRepository<Book>
    {

        private const string _path = @"C:\\Users\\User\\OneDrive\\Documentos\\codeacademy\\LibarySystemManag\\Data\\books.txt";
        private const int ID_LENGTH = 5;
        //private const int MEMBER_ID_LENGTH = 5;

        private const int TITLE_LENGTH = 30;
        private const int AUTHOR_LENGTH = 25;
        private const int ISBN_LENGTH = 13;
        private const int YEAR_LENGTH = 4;
        private const int CATEGORY_ID_LENGTH = 5;
        private const int IS_AVAILABLE_LENGTH = 1;

        public BookRepository()
        {
            string directory = Path.GetDirectoryName(_path);
            if(!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(_path))
            {
                File.Create(_path).Close();

            }
        }
        public void Add(Book entity)
        {
            var books = GetAll();
            entity.Id = books.Any() ? books.Max(x => x.Id) + 1 : 1;
            entity.IsAvailable = true;
            string line  = ConvertLine(entity);
            File.AppendAllText(_path, line + Environment.NewLine);
        }

       
        public void Delete(int id)
        {
            var books = GetAll();
            int removeCount = books.RemoveAll(x => x.Id == id);
            if(removeCount== 0)
            {
                throw new Exception("Kitab tapılmadı!");
            }
            SaveAll(books);
        }

        public List<Book> GetAll()
        {
          var books = new List<Book>();
            if (!File.Exists(_path))
                    return books;

            foreach (var line in File.ReadAllLines(_path))
                if(!string.IsNullOrWhiteSpace(line))
                {
                    books.Add(ParseFromLine(line));
                }
            return books;
        }

        public Book GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public List<Book> Search(string keyword)
        {
            var books = GetAll();
            if(string.IsNullOrWhiteSpace(keyword))
                  return books;

            keyword = keyword.ToLower();

             return books.Where(x => 
             x.Title.ToLower().Contains(keyword) || 
             x.Author.ToLower().Contains(keyword) ||  
             x.ISBN.Contains(keyword)).ToList();
        }

        public void Uptade(Book entity)
        {
            var books = GetAll();
            var index = books.FindIndex(x => x.Id == entity.Id);
            if(index == -1)
            {
                throw new Exception("Kitab tapılmadı!");
            }
            books[index] = entity;
            SaveAll(books);
        }

        private string ConvertLine(Book book)
        {
            string id = book.Id.ToString().PadLeft(ID_LENGTH, '0');
            string title = book.Title.ToString().PadRight(TITLE_LENGTH);
            string author = book.Author.ToString().PadRight(AUTHOR_LENGTH);
            string isbn = book.ISBN.ToString().PadRight(ISBN_LENGTH);
            string year = book.PublishedYear.ToString().PadRight(YEAR_LENGTH,'0');
            string categoryId = book.CategoryId.ToString().PadRight(CATEGORY_ID_LENGTH,'0');
            //string memberId = (book.MemberId ?? 0)
            //.ToString()
            //.PadRight(MEMBER_ID_LENGTH, '0');
            //string memberId = (book.MemberId ?? 0).ToString().PadLeft(MEMBER_ID_LENGTH, '0');
            //string memberId = (book.MemberId ?? 0).ToString().PadLeft(MEMBER_ID_LENGTH, '0');
            string isAvailble =  book.IsAvailable ? "1" : "0";

            return id + title + author + isbn + year + categoryId + isAvailble;

        }

        private Book ParseFromLine(string line)
        {
            if (line.Length < ID_LENGTH + TITLE_LENGTH + AUTHOR_LENGTH + ISBN_LENGTH+YEAR_LENGTH +CATEGORY_ID_LENGTH   + IS_AVAILABLE_LENGTH)
            {
                throw new Exception("Fayl Formati Duzgun Deyil!");
            }
            int pos = 0;

            Book book = new Book();
            book.Id = int.Parse(line.Substring(pos, ID_LENGTH).Trim());
            pos += ID_LENGTH;
            book.Title = line.Substring(pos,TITLE_LENGTH).Trim();
            pos+= TITLE_LENGTH;
            book.Author=line.Substring(pos,AUTHOR_LENGTH).Trim();
            pos += AUTHOR_LENGTH;
            book.ISBN = line.Substring (pos,ISBN_LENGTH).Trim();
            pos += ISBN_LENGTH;
            book.PublishedYear = int.Parse(line.Substring(pos,YEAR_LENGTH).Trim());
            pos += YEAR_LENGTH;
            book.CategoryId = int.Parse(line.Substring(pos,CATEGORY_ID_LENGTH).Trim());
            
            book.IsAvailable = line.Substring(pos, IS_AVAILABLE_LENGTH) == "1";
            

            return book;
        }

        private void SaveAll(List<Book> books)
        {
            var lines = books.Select(b => ConvertLine(b)).ToList();
            File.WriteAllLines(_path, lines);
        }
    }
}
