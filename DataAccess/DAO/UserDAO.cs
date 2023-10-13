using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class UserDAO
    {
        private EbookStoreDbContext _context;

        public UserDAO(EbookStoreDbContext context)
        {
            _context = context;
        }

        public int AddUser(User user)
        {
            if (user != null)
            {
                _context.Users.Add(new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    MiddleName = user.MiddleName,
                    Email = user.Email,
                    Password = user.Password,
                    Source = user.Source,
                    RoleId = 1,
                    PubId = user.PubId,
                    HireDate = user.HireDate
                });
                return 1;
            }
            return 0;
        }

        public bool UpdateUser(User user)
        {
            if (user != null)
            {
                User? u = GetUserById(user.UserId);
                if (u != null)
                {
                    u.FirstName = user.FirstName;
                    u.LastName = user.LastName;
                    u.MiddleName = user.MiddleName;
                    u.Email = user.Email;
                    if(user.Password != null && user.Password.Length > 0) u.Password = user.Password;
                    u.Source = user.Source;
                    if (user.RoleId > 0) u.RoleId = user.RoleId;
                    u.PubId = user.PubId;
                    u.HireDate = user.HireDate;
                    return true;
                }
            }
            return false;
        }

        public bool DeleteUser(int id)
        {
            if (id != 0)
            {
                User? u = GetUserById(id);
                if (u != null)
                {
                    _context.Users.Remove(u);
                    return true;
                }
            }
            return false;
        }

        public List<User> GetAllUsers()
        {
            return _context.Users
                .Include(user => user.Role)
                .Include(user => user.Publisher)
                .ToList();
        }

        public User? GetUserById(int id)
        {
            return _context.Users
                .Include(user => user.Role)
                .Include(user => user.Publisher)
                .FirstOrDefault(u => u.UserId == id);
        }

        public User? GetUserByEmail(string? email)
        {
            return _context.Users
                .Include(user => user.Role)
                .FirstOrDefault(u => u.Email.Equals(email));
        }
    }
}
