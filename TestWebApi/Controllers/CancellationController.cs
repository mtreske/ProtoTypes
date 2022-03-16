using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace TestWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CancellationController : ControllerBase
    {
        private readonly ILogger _logger;

        public CancellationController(ILogger<CancellationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<string> Get(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting to do slow work");
            await Task.Delay(10000, cancellationToken);

            var message = "Finished slow delay of 10 seconds.";

            _logger.LogInformation(message);

            return message;
        }
    }
}