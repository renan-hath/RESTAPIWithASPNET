using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RESTWithNET8.Businesses;
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
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        [HttpGet("{id}")]
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
        public IActionResult Post([FromBody] Book book)
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
        public IActionResult Put([FromBody] Book book)
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
