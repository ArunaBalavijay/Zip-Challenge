using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RawRabbit;
using Zip.Challenge.Common.Dto;

namespace Zip.Challenge.Services.Identity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthCheckController : ControllerBase
    {
        private readonly ILogger<HealthCheckController> _logger;
        private readonly IBusClient _busClient;

        public HealthCheckController(ILogger<HealthCheckController> logger, IBusClient busClient)
        {
            _logger = logger;
            _busClient = busClient;
        }

        [HttpGet]
        public HealthCheck Get()
        {
            return new HealthCheck { Status = "Healthy" };
        }
    }
}
