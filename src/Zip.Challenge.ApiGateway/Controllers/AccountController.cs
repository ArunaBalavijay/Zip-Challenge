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
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public AccountController(IAccountService accountService, IUserService userService)
        {
            _accountService = accountService;
            _userService = userService;
        }

        // GET: /<controller>
        [HttpGet]
        public async Task<IEnumerable<Account>> Get()
        {
            return await _accountService.ListAsync();
        }

        // POST /<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateAccount command)
        {
            var user = await _userService.GetAsync(command.UserEmail);

            if (user == null)
            {
                return BadRequest(
                    new ValidationResult(400, new Dictionary<string, string>
                    {
                        { "Email", UserValidation.UserDoesNotExistError }
                    })
                );
            }
            else if(_accountService.CanCreateCustomerAccount(user))
            {
                return UnprocessableEntity(
                    new ValidationResult(422, new Dictionary<string, string>
                    {
                        { "Email", AccountValidation.CustomerEligibiltyError }
                    })
                );
            }

            await _accountService.CreateAsync(command);
            return Accepted(new ValidationResult(202));
        }
    }
}
