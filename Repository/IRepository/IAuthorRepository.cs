using BusinessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IAuthorRepository
    {
        IQueryable<AuthorDTO> GetAuthors();
        AuthorDTO? GetAuthor(int id);
        void UpdateAuthor(AuthorDTO author);
        bool SaveAuthor(AuthorCreateDTO author);
        bool DeleteAuthor(int id);
    }
}
