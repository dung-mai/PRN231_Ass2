using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class BookUpdateDTO
    {
        public int BookId { get; set; }
        public string Title { get; set; } = null!;
        public string Type { get; set; } = null!;
        public int PubId { get; set; }
        public decimal Price { get; set; }
        public string Advance { get; set; } = null!;
        public decimal Royalty { get; set; }
        public string YtdSales { get; set; } = null!;
        public string Notes { get; set; } = null!;
        public DateTime PublishedDate { get; set; }
    }
}
