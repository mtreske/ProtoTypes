using Microsoft.AspNetCore.Mvc;

namespace TestWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayloadController : ControllerBase
    {
        [HttpGet]
        public string GetString()
        {
            return "anyString";
        }
    }
}