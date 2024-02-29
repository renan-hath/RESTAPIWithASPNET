using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RESTWithNET8.Models;
using RESTWithNET8.Businesses;
using RESTWithNET8.Data.ValueObjects;

namespace RESTWithNET8.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;

        // Declaration of the business used
        private IPersonBusiness _personBusiness;

        // Injection of an instance of IPersonBusiness when creating an instance of PersonController
        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBusiness = personBusiness;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindByID(id);

            if (person == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(person);
            }
        }

        // [FromBody] consumes the JSON object sent in the request body
        [HttpPost]
        public IActionResult Post([FromBody] PersonVO person)
        {
            if (person == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(_personBusiness.Create(person));
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] PersonVO person)
        {
            if (person == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(_personBusiness.Update(person));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var person = _personBusiness.FindByID(id);

            if (person == null)
            {
                return NotFound();
            }
            else
            {
                _personBusiness.Delete(id);
                return NoContent();
            }
        }
    }
}
