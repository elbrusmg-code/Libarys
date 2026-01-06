using BusinessLogicLayer.Dtos;
using BusinessLogicLayer.Services.Contracts;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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

        public void Add(BookCteateDto bookDto)
        {
            var book = new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                ISBN = bookDto.ISBN,
                PublishedYear = bookDto.PublishedYear,
                CategoryId = bookDto.CategoryId,
                IsAvailable = true
            };

            ValidateBook(book);
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

        public void Update(BookUptadeDto bookUp)
        {
            var book = _bookRepository.GetById(bookUp.Id);
            if (book == null)
                throw new Exception("Kitab tapılmadı!");

            book.Title = bookUp.Title;
            book.Author = bookUp.Author;
            book.ISBN = bookUp.ISBN;
            book.PublishedYear = bookUp.PublishedYear;
            book.CategoryId = bookUp.CategoryId;

            ValidateBook(book);
            _bookRepository.Uptade(book);
        }

        public void BorrowBook(int bookId, int memberId)
        {
            try
            {
                var book = _bookRepository.GetById(bookId);
                if (book == null)
                    throw new Exception("Kitab tapılmadı!");

                if (!book.IsAvailable)
                    throw new Exception("Kitab artıq götürülüb!");

                var member = _memberRepository.GetById(memberId);
                if (member == null || !member.IsActive)
                    throw new Exception("Üzv mövcud deyil və ya aktiv deyil!");

                book.MemberId = memberId;
                book.IsAvailable = false;

                _bookRepository.Uptade(book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        

        public void ReturnBook(int bookId)
        {
            var book = _bookRepository.GetById(bookId);
            if (book == null)
                throw new Exception("Kitab tapılmadı!");

            if (book.IsAvailable)
                throw new Exception("Bu kitab artıq kitabxanadadır!");

            book.IsAvailable = true;

            _bookRepository.Uptade(book);
        }

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
          
            if (!IsValidISBN(book.ISBN))
            {
                throw new Exception("ISBN düzgün deyil! (ISBN-10 və ya ISBN-13 olmalıdır)");
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
        private bool IsValidISBN(string isbn)
        {
            isbn = isbn.Replace("-", "").Replace(" ", "");

            return isbn.Length == 10
                ? IsValidISBN10(isbn)
                : isbn.Length == 13 && IsValidISBN13(isbn);
        }

        private bool IsValidISBN10(string isbn)
        {
            if (!Regex.IsMatch(isbn, @"^\d{9}[\dX]$"))
                return false;

            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += (isbn[i] - '0') * (10 - i);

            int check = isbn[9] == 'X' ? 10 : isbn[9] - '0';
            sum += check;

            return sum % 11 == 0;
        }

        private bool IsValidISBN13(string isbn)
        {
            if (!Regex.IsMatch(isbn, @"^\d{13}$"))
                return false;

            int sum = 0;
            for (int i = 0; i < 12; i++)
                sum += (isbn[i] - '0') * ((i % 2 == 0) ? 1 : 3);

            int check = (10 - (sum % 10)) % 10;
            return check == isbn[12] - '0';
        }
    }


}

