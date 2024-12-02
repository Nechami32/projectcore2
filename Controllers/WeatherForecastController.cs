using Microsoft.AspNetCore.Mvc;
using OurApi.Models;

namespace OurApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private static List<WeatherForecast> arr;
    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }
    static WeatherForecastController()
    {
        arr = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToList();
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return arr;
    }
    
    [HttpGet("{id}")]
    public WeatherForecast Get(int id)
    {
        return arr[id];
    }

    [HttpPost]
    public void Post(WeatherForecast newItem)
    {
        arr.Add(newItem);
    }

    [HttpPut("{id}")]
    public void Put(int id, WeatherForecast newItem)
    {
        arr[id]=newItem;
    }   

    [HttpDelete("{id}")]
    public void RuthDelete(int id)
    {
        arr.RemoveAt(id);
    } 
}
