using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.DAO;
using Repository.IRepository;

namespace Repository.Repository
{
    public class BookAuthorRepository : IBookAuthorRepository
    {
        EbookStoreDbContext _context;
        IMapper _mapper;

        public BookAuthorRepository(EbookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool AddBookAuthor(BookAuthorRequestDTO bookAuthor)
        {
            try
            {
                BookAuthorDAO bookAuthorDAO = new BookAuthorDAO(_context);
                bool result = bookAuthorDAO.Add(_mapper.Map<BookAuthor>(bookAuthor));
                if (result)
                {
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
