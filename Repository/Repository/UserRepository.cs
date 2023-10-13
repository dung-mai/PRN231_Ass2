using AutoMapper;
using AutoMapper.Execution;
using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.DAO;
using Repository.IRepository;

namespace Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        readonly EbookStoreDbContext _context;
        readonly IMapper _mapper;

        public UserRepository(EbookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool DeleteUser(int id)
        {
            try
            {
                UserDAO userDAO = new(_context);
                userDAO.DeleteUser(id);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public UserResponseDTO? GetUser(int id)
        {
            UserDAO userDAO = new(_context);
            return _mapper.Map<UserResponseDTO>(userDAO.GetUserById(id));
        }

        public IQueryable<UserResponseDTO> GetUsers()
        {
            UserDAO userDAO = new(_context);
            List<User> users = userDAO.GetAllUsers();
            return users.Select(p => _mapper.Map<UserResponseDTO>(p)).AsQueryable();
        }

        public bool SaveUser(UserCreateDTO user)
        {
            try
            {
                UserDAO userDAO = new(_context);
                int result = userDAO.AddUser(_mapper.Map<User>(user));
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
            catch (Exception)
            {
                return false;
            }
        }

        public void UpdateUser(UserUpdateDTO user)
        {
            UserDAO userDAO = new(_context);
            userDAO.UpdateUser(_mapper.Map<User>(user));
            _context.SaveChanges();
        }

        public UserResponseDTO? Login(string email, string password)
        {
            UserDAO userDAO = new(_context);
            User? user = userDAO.GetUserByEmail(email);
            return (user != null && user.Password.Equals(password)) ? _mapper.Map<UserResponseDTO>(user) : null;
        }
    }
}
