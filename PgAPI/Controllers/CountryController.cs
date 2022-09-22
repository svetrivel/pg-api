using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace PgAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountryController : ControllerBase
{
    private readonly ILogger<CountryController> _logger;
    private readonly PGApiContext _repository;

    public CountryController(ILogger<CountryController> logger, PGApiContext repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet]
    public IEnumerable<Object> GetAllCountries()
    {
        return _repository.Countries.Select(x => new
        {
            Id = x.Id,
            Name = x.Name,
            ShortName = x.ShortName
        });
    }

    [HttpGet]
    [Route("GetCountryByShortName")]
    public IEnumerable<Object> GetCountryByShortName(string shortName)
    {
        return _repository.Countries.
        Where(x => x.ShortName == shortName).Select(x => new
        {
            Id = x.Id,
            Name = x.Name,
            ShortName = x.ShortName
        });
    }

    [HttpPost]
    public ActionResult CreateCountry(Country countryData)
    {
        var country = _repository.Countries.FirstOrDefault(x => x.ShortName == countryData.ShortName);

        if (country != default)
            return new OkObjectResult(new { Country = countryData.Name, error = $"{countryData.ShortName} already exists!." });

        var newCountry = new Country();
        newCountry.Name = countryData.Name;
        newCountry.ShortName = countryData.ShortName;

        _repository.Countries.Add(newCountry);
        _repository.SaveChanges();

        return new OkObjectResult(new { Message = "Country created successfully!", Id = newCountry.Id });
    }
}
