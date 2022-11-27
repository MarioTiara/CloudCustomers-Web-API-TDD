using Microsoft.AspNetCore.Mvc;

namespace CloudCustomers.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetUsers")]
        public Task<IActionResult> Get()
        {
            return null;
        }
    }
}