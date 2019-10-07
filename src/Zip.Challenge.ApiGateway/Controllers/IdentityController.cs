using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Challenge.ApiGateway.Services;
using Zip.Challenge.ApiGateway.Validations;
using Zip.Challenge.Common.Commands;
using Zip.Challenge.Common.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Zip.Challenge.ApiGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IUserService _userService;

        public IdentityController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: /<controller>
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _userService.ListAsync();
        }

        // GET /<controller>/1@1.com
        [HttpGet("{email}")]
        public async Task<User> Get(string email)
        {
            var user = await _userService.GetAsync(email);
            return user ?? new User();
        }

        // POST /<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateUser command)
        {
            var user = await _userService.GetAsync(command.Email);

            if (user != null)
            {
                return UnprocessableEntity(
                    new ValidationResult(422, new Dictionary<string, string>
                    {
                        { "Email", UserValidation.UserExistsError }
                    })
                );
            }

            await _userService.RegisterAsync(command);
            return Accepted(new ValidationResult(202));
        }
    }
}
