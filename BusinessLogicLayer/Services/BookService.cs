using BusinessLogicLayer.Services.Contracts;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Services
{
    public class BookService : IBookService
    {

        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Member> _memberRepository;
        public BookService(IRepository<Book> bookRepository, IRepository<Category> categoryRepository, IRepository<Member> memberRepository )
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
            _memberRepository = memberRepository;
        }

        public BookService(BookRepository bookRepository, CategoryRepository categoryRepository)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
        }

        public void Add(Book book)
        {
            ValidateBook(book);
           var category = _categoryRepository.GetById(book.CategoryId);
            if(category == null)
            {
                throw new Exception($"Kateqoriya tapılmadı! CategoryId: {book.CategoryId}");
            }

            var existingBooks = _bookRepository.GetAll();
            if(existingBooks.Any(b => b.ISBN == book.ISBN) )
            {
                throw new Exception($"Bu ISBN artıq mövcuddur: {book.ISBN}");
            }
            book.IsAvailable = true;
            _bookRepository.Add(book);
        }

        public void Delete(int id)
        {
            if(id <= 0)
            {
                throw new Exception("ID Müsbət olmalıdır!");
            }
            var books =_bookRepository.GetById(id);
            if(books == null)
            {
                throw new Exception($"Kitab tapılmadı! ID: {id}");
            }
            _bookRepository.Delete(id);
        }

        public List<Book> GetAll()
        {
            return _bookRepository.GetAll();
        }

        public Book GetById(int id)
        {
            if( id <= 0 )
            {
                throw new Exception("ID Müsbət olmalıdır!");
            }
            var books = _bookRepository.GetById(id);
            if (books == null)
            {
                throw new Exception($"Kitab tapılmadı! ID: {id}");
            }
            return books;
        }

        public List<Book> Search(string keyword)
        {
            if(string.IsNullOrWhiteSpace(keyword))
            {
                return GetAll();
            }
            return _bookRepository.Search(keyword);
        }

        public void Update(Book book)
        {
           if(book.Id <= 0)
            {
                throw new Exception("ID müsbət olmalıdır!");
            }
            var existingBook = _bookRepository.GetById(book.Id);
            if (existingBook == null)
            {
                throw new Exception($"Kitab tapılmadı! ID: {book.Id}");
            }
            ValidateBook(book);

            var category = _categoryRepository.GetById(book.CategoryId);
            if (category == null)
            {
                throw new Exception($"Kateqoriya tapılmadı! CategoryId: {book.CategoryId}");
            }

            var existingBooks = _bookRepository.GetAll();
            if (existingBooks.Any(b => b.Id != book.Id && b.ISBN == book.ISBN))
            {
                throw new Exception($"Bu ISBN artıq istifadə olunur: {book.ISBN}");
            }
         


            _bookRepository.Uptade(book);
        }

        //public void BorrowBook(int bookId, int memberId)
        //{
        //   try
        //    {
        //        var book = _bookRepository.GetById(bookId);
        //        if (book == null)
        //            throw new Exception("Kitab tapılmadı!");

        //        if (!book.IsAvailable)
        //            throw new Exception("Kitab artıq götürülüb!");

        //        var member = _memberRepository.GetById(memberId);
        //        if (member == null || !member.IsActive)
        //            throw new Exception("Üzv mövcud deyil və ya aktiv deyil!");

        //        book.MemberId = memberId;
        //        book.IsAvailable = false;

        //        _bookRepository.Uptade(book);
        //    }
        //    catch
        //    {
        //        throw new Exception("Islemir");
        //    }
        //}

        //public void ReturnBook(int bookId)
        //{
        //    var book = _bookRepository.GetById(bookId);
        //    if (book == null)
        //        throw new Exception("Kitab tapılmadı!");

        //    if (book.IsAvailable)
        //        throw new Exception("Bu kitab artıq kitabxanadadır!");

        //    book.IsAvailable = true;

        //    _bookRepository.Uptade(book);
        //}

        private void ValidateBook(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
            {
                throw new Exception("Kitab adı boş ola bilməz!");
            }
            if (book.Title.Length > 30)
            {
                throw new Exception("Kitab adı maksimum 30 simvol ola bilər!");
            }

            if (string.IsNullOrWhiteSpace(book.Author))
            {
                throw new Exception("Müəllif adı boş ola bilməz!");
            }
            if (book.Author.Length > 25)
            {
                throw new Exception("Müəllif adı maksimum 25 simvol ola bilər!");
            }

            if (string.IsNullOrWhiteSpace(book.ISBN))
            {
                throw new Exception("ISBN boş ola bilməz!");
            }
            if (book.ISBN.Length != 13)
            {
                throw new Exception("ISBN 13 simvol olmalıdır!");
            }
            if (!book.ISBN.All(char.IsDigit))
            {
                throw new Exception("ISBN yalnız rəqəmlərdən ibarət olmalıdır!");
            }

            if (book.PublishedYear < 1500 || book.PublishedYear > DateTime.Now.Year)
            {
                throw new Exception($"Nəşr ili 1500 ilə {DateTime.Now.Year} arasında olmalıdır!");
            }

            if (book.CategoryId <= 0)
            {
                throw new Exception("Kateqoriya seçilməlidir!");
            }
        }
    }
}
