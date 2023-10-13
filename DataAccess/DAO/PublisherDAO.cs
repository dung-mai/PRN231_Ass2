using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class PublisherDAO
    {
        EbookStoreDbContext _context;
        public PublisherDAO(EbookStoreDbContext context)
        {
            _context = context;
        }

        public List<Publisher> GetPublishers()
        {
            return _context.Publishers.ToList();
        }

        public Publisher? GetPublisher(int id)
        {
            return _context.Publishers.FirstOrDefault(p => p.PubId == id);
        }

        public int AddPublisher(Publisher publisher)
        {
            if (publisher != null)
            {
                _context.Publishers.Add(publisher);
                return 1;
            }
            return 0;
        }

        public int DeletePublisher(int publisherId)
        {
            Publisher? publisher = _context.Publishers.FirstOrDefault(p => p.PubId == publisherId);
            if (publisher != null)
            {
                _context.Publishers.Remove(publisher);
                return 1;
            }
            return 0;
        }

        public int UpdatePublisher(Publisher _publisher)
        {
            Publisher? publisher = _context.Publishers
                .FirstOrDefault(p => p.PubId == _publisher.PubId);
            if (publisher != null)
            {
                publisher.PublisherName = _publisher.PublisherName;
                publisher.City = _publisher.City;
                publisher.State = _publisher.State;
                publisher.Country = _publisher.Country;
                return 1;
            }
            return 0;
        }
    }
}
