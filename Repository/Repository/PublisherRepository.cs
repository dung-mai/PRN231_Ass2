using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.DAO;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;

namespace Repository.Repository
{
    public class PublisherRepository : IPublisherRepository
    {
        EbookStoreDbContext _context;
        IMapper _mapper;

        public PublisherRepository(EbookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool DeletePublisher(int id)
        {
            try
            {
                PublisherDAO publisherDAO = new PublisherDAO(_context);
                publisherDAO.DeletePublisher(id);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public PublisherDTO? GetPublisher(int id)
        {
            PublisherDAO publisherDAO = new PublisherDAO(_context);
            return _mapper.Map<PublisherDTO>(publisherDAO.GetPublisher(id));
        }

        public IQueryable<PublisherDTO> GetPublishers()
        {
            PublisherDAO publisherDAO = new PublisherDAO(_context);
            List<Publisher> publishers = publisherDAO.GetPublishers();
            return publishers.Select(p => _mapper.Map<PublisherDTO>(p)).AsQueryable();
        }

        public bool SavePublisher(PublisherCreateDTO publisher)
        {
            try
            {
                PublisherDAO publisherDAO = new PublisherDAO(_context);
                int result = publisherDAO.AddPublisher(_mapper.Map<Publisher>(publisher));
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

        public void UpdatePublisher(PublisherDTO publisher)
        {
            PublisherDAO publisherDAO = new PublisherDAO(_context);
            publisherDAO.UpdatePublisher(_mapper.Map<Publisher>(publisher));
            _context.SaveChanges();
        }
    }
}
