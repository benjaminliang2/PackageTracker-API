using Microsoft.AspNetCore.Mvc;

namespace PackageTracker_API.Controllers
{

    [Route("api/v0/[controller]")]
    [ApiController]

    public class PackageController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PackageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet("{city}")]
        public async Task<IActionResult> GetWeather(string city)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                string apiKEY = "0accd00ee96f87579969974f2b12729d";
                string apiURL = "https://api.openweathermap.org/data/2.5/weather?q=" + city + "&appid=" + apiKEY;
                var response = await client.GetAsync(apiURL);
                if (response.IsSuccessStatusCode)
                {
                    var weatherData = await response.Content.ReadAsStringAsync();
                    return Ok(weatherData);

                }
                else
                {
                    return BadRequest("Error fetching weather data");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured: {ex.Message}");

            }
        }
    }
    public class WeatherResponse
    {
        public string Name { get; set; }
        public MainData Main { get; set; }
    }
    public class MainData
    {
        public double Temp { get; set; }
    }

}
