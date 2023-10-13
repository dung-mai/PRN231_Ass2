using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class AuthorDAO
    {
        EbookStoreDbContext _context;
        public AuthorDAO(EbookStoreDbContext context)
        {
            _context = context;
        }

        public List<Author> GetAuthors()
        {
            return _context.Authors.ToList();
        }

        public Author? GetAuthor(int id)
        {
            return _context.Authors.FirstOrDefault(a => a.AuthorId == id);
        }

        public int AddAuthor(Author author)
        {
            if (author != null)
            {
                _context.Authors.Add(author);
                return 1;
            }
            return 0;
        }

        public int DeleteAuthor(int authorId)
        {
            Author? author = _context.Authors.FirstOrDefault(a => a.AuthorId == authorId);
            if (author != null)
            {
                _context.Authors.Remove(author);
                return 1;
            }
            return 0;
        }

        public int UpdateAuthor(Author _author)
        {
            Author? author = _context.Authors
                .FirstOrDefault(a => a.AuthorId == _author.AuthorId);
            if (author != null)
            {
                author.FirstName = _author.FirstName;
                author.LastName = _author.LastName;
                author.Phone = _author.Phone;
                author.EmailAddress = _author.EmailAddress;
                author.Address = _author.Address;
                author.City = _author.City;
                author.State = _author.State;
                author.Zip = _author.Zip;
                return 1;
            }
            return 0;
        }
    }
}
