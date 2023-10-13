using BusinessObject.DTO;

namespace Repository.IRepository
{
    public interface IPublisherRepository
    {
        IQueryable<PublisherDTO> GetPublishers();
        PublisherDTO? GetPublisher(int id);
        void UpdatePublisher(PublisherDTO publisher);
        bool SavePublisher(PublisherCreateDTO publisher);
        bool DeletePublisher(int id);
    }
}
