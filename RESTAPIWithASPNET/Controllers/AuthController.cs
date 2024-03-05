using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTWithNET8.Businesses;
using RESTWithNET8.Data.ValueObjects;
using RESTWithNET8.Models;

namespace RESTWithNET8.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ILoginBusiness _loginBusiness;

        public AuthController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] UserVO userVO)
        {
            if (userVO == null)
            {
                return BadRequest("Invalid client request");
            }

            var token = _loginBusiness.ValidateCredentials(userVO);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenVO tokenVO)
        {
            if (tokenVO == null)
            {
                return BadRequest("Invalid client request");
            }

            var token = _loginBusiness.ValidateCredentials(tokenVO);

            if (token == null)
            {
                return BadRequest("Invalid client request");
            }

            return Ok(token);
        }

        [HttpGet]
        [Authorize("Bearer")]
        [Route("revoke")]
        public IActionResult Revoke()
        {
            var username = User.Identity.Name;
            var result = _loginBusiness.RevokeToken(username);

            if (!result)
            {
                return BadRequest("Invalid client request");
            }

            return NoContent();
        }
    }
}
