using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.DAO;
using Repository.IRepository;

namespace Repository.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        EbookStoreDbContext _context;
        IMapper _mapper;

        public AuthorRepository(EbookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool DeleteAuthor(int id)
        {
            try
            {
                AuthorDAO authorDAO = new AuthorDAO(_context);
                authorDAO.DeleteAuthor(id);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public AuthorDTO? GetAuthor(int id)
        {
            AuthorDAO authorDAO = new AuthorDAO(_context);
            return _mapper.Map<AuthorDTO>(authorDAO.GetAuthor(id));
        }

        public IQueryable<AuthorDTO> GetAuthors()
        {
            AuthorDAO authorDAO = new AuthorDAO(_context);
            List<Author> authors = authorDAO.GetAuthors();
            return authors.Select(a => _mapper.Map<AuthorDTO>(a)).AsQueryable();
        }

        public bool SaveAuthor(AuthorCreateDTO author)
        {
            try
            {
                AuthorDAO authorDAO = new AuthorDAO(_context);
                int result = authorDAO.AddAuthor(_mapper.Map<Author>(author));
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

        public void UpdateAuthor(AuthorDTO author)
        {
            AuthorDAO authorDAO = new(_context);
            authorDAO.UpdateAuthor(_mapper.Map<Author>(author));
            _context.SaveChanges();
        }
    }
}
