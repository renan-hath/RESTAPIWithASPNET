using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RESTWithNET8.Businesses;
using RESTWithNET8.Data.ValueObjects;
using RESTWithNET8.Hypermedia.Filters;
using RESTWithNET8.Models;

namespace RESTWithNET8.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;

        // Declaration of the business used
        private IBookBusiness _bookBusiness;

        // Injection of an instance of IBookBusiness when creating an instance of BookController
        public BookController(ILogger<BookController> logger, IBookBusiness bookBusiness)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
        }

        [HttpGet]
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var book = _bookBusiness.FindByID(id);

            if (book == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(book);
            }
        }

        // [FromBody] consumes the JSON object sent in the request body
        [HttpPost]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] BookVO book)
        {
            if (book == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(_bookBusiness.Create(book));
            }
        }

        [HttpPut]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] BookVO book)
        {
            if (book == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(_bookBusiness.Update(book));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
            var book = _bookBusiness.FindByID(id);

            if (book == null)
            {
                return NotFound();
            }
            else
            {
                _bookBusiness.Delete(id);
                return NoContent();
            }
        }
    }
}
