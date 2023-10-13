using BusinessObject.DTO;

namespace Repository.IRepository
{
    public interface IUserRepository
    {
        public UserResponseDTO? Login(string email, string password);
        IQueryable<UserResponseDTO> GetUsers();
        UserResponseDTO? GetUser(int id);
        void UpdateUser(UserUpdateDTO user);
        bool SaveUser(UserCreateDTO user);
        bool DeleteUser(int id);
    }
}
