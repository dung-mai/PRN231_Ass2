using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.DAO;
using Repository.IRepository;

namespace Repository.Repository
{
    public class BookRepository : IBookRepository
    {
        EbookStoreDbContext _context;
        IMapper _mapper;

        public BookRepository(EbookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool DeleteBook(int id)
        {
            try
            {
                BookDAO bookDAO = new BookDAO(_context);
                bookDAO.DeleteBook(id);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public BookResponseDTO? GetBook(int id)
        {
            BookDAO bookDAO = new BookDAO(_context);
            return _mapper.Map<BookResponseDTO>(bookDAO.GetBook(id));
        }

        public IQueryable<BookResponseDTO> GetBooks()
        {
            BookDAO bookDAO = new BookDAO(_context);
            List<Book> books = bookDAO.GetBooks();
            return books.Select(b => _mapper.Map<BookResponseDTO>(b)).AsQueryable();
        }

        public bool SaveBook(BookCreateDTO book)
        {
            try
            {
                BookDAO bookDAO = new BookDAO(_context);
                int result = bookDAO.AddBook(_mapper.Map<Book>(book));
                if (result > 0)
                {
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void UpdateBook(BookUpdateDTO book)
        {
            BookDAO bookDAO = new BookDAO(_context);
            bookDAO.UpdateBook(_mapper.Map<Book>(book));
            _context.SaveChanges();
        }
    }
}
