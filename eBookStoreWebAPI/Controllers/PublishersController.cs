using BusinessObject.DTO;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;

namespace ePublisherStoreWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherRepository _publisherRepository;

        public PublishersController(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        // GET: api/Publishers
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<PublisherDTO>> GetPublishers()
        {
            return Ok(_publisherRepository.GetPublishers());
        }

        [HttpPost]
        public IActionResult PostPublisher([FromBody] PublisherCreateDTO publisher)
        {
            if (_publisherRepository.SavePublisher(publisher))
            {
                return NoContent();
            }
            else
            {
                return Problem("Problem when Adding Publisher");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePublisher([FromBody] int id)
        {
            var publisher = _publisherRepository.GetPublisher(id);
            if (publisher == null)
            {
                return NotFound();
            }

            //_publisherRepository.DeletePublisher(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult PutPublisher(int id, PublisherDTO publisher)
        {
            if (id != publisher.PubId)
            {
                return BadRequest();
            }

            _publisherRepository.UpdatePublisher(publisher);
            return NoContent();
        }
    }
}
