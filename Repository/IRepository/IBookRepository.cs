using BusinessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IBookRepository
    {
        IQueryable<BookResponseDTO> GetBooks();
        BookResponseDTO? GetBook(int id);
        void UpdateBook(BookUpdateDTO book);
        bool SaveBook(BookCreateDTO book);
        bool DeleteBook(int id);
    }
}
