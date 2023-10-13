using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class Book
    {
        public Book()
        {
            BookAuthors = new HashSet<BookAuthor>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }
        public string Title { get; set; } = null!;
        public string Type { get; set; } = null!;
        [ForeignKey("Publisher")]
        public int PubId { get; set; }
        public decimal Price { get; set; }
        public string Advance { get; set; } = null!;
        public decimal Royalty { get; set; }
        public string YtdSales { get; set; } = null!;
        public string Notes { get; set; } = null!;
        public DateTime PublishedDate { get; set; }

        public virtual Publisher Publisher { get; set; } = null!;
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
