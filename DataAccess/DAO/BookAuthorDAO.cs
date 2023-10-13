using BusinessObject.Models;

namespace DataAccess.DAO
{
    public class BookAuthorDAO
    {
        private EbookStoreDbContext _context;

        public BookAuthorDAO(EbookStoreDbContext context)
        {
            _context = context;
        }

        public bool Add(BookAuthor bookAuthor)
        {
            try
            {
                _context.BookAuthors.Add(bookAuthor);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
