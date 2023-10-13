using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repository.IRepository;

namespace eBookStoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        // GET: api/Authors
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<AuthorDTO>> GetAuthors()
        {
            return Ok(_authorRepository.GetAuthors());
        }

        //// GET: api/Authors/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Author>> GetAuthor(int id)
        //{

        //    var author = await _context.Authors.FindAsync(id);

        //    if (author == null)
        //    {
        //        return NotFound();
        //    }

        //    return author;
        //}

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutAuthor(int id, AuthorDTO author)
        {
            if (id != author.AuthorId)
            {
                return BadRequest();
            }

            _authorRepository.UpdateAuthor(author);
            return NoContent();
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostAuthor(AuthorCreateDTO author)
        {
            if (_authorRepository.SaveAuthor(author))
            {
                return NoContent();
            }
            else
            {
                return Problem("Problem when Adding Author");
            }
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            var author = _authorRepository.GetAuthor(id);
            if (author == null)
            {
                return NotFound();
            }

            _authorRepository.DeleteAuthor(id);
            return NoContent();
        }
    }
}
