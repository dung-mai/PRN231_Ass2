using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class BookDAO
    {
        EbookStoreDbContext _context;
        public BookDAO(EbookStoreDbContext context)
        {
            _context = context;
        }

        public List<Book> GetBooks()
        {
            return _context.Books
                .Include(book => book.Publisher)
                .ToList();
        }

        public Book? GetBook(int id)
        {
            return _context.Books
                .Include(book => book.Publisher)
                .FirstOrDefault(b => b.BookId == id);
        }

        public int AddBook(Book book)
        {
            if (book != null)
            {
                _context.Books.Add(book);
                return 1;
            }
            return 0;
        }

        public int DeleteBook(int bookId)
        {
            Book? book = _context.Books.FirstOrDefault(b => b.BookId == bookId);
            if (book != null)
            {
                _context.Books.Remove(book);
                return 1;
            }
            return 0;
        }

        public int UpdateBook(Book _book)
        {
            Book? book = _context.Books
                .FirstOrDefault(b => b.BookId == _book.BookId);
            if (book != null)
            {
                book.Title = _book.Title;
                book.Type = _book.Type;
                book.PubId = _book.PubId;
                book.Price = _book.Price;
                book.Advance = _book.Advance;
                book.Royalty = _book.Royalty;
                book.YtdSales = _book.YtdSales;
                book.Notes = _book.Notes;
                book.PublishedDate = _book.PublishedDate;
                return 1;
            }
            return 0;
        }
    }
}
