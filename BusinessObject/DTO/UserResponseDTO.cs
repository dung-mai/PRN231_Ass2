using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class UserResponseDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Source { get; set; } = null!;
        public int RoleId { get; set; }
        public int PubId { get; set; }
        public DateTime HireDate { get; set; }

        public virtual RoleDTO Role { get; set; } = null!;
        public virtual PublisherDTO Publisher { get; set; } = null!;
    }
}
