using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace PgAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StateController : ControllerBase
{
    private readonly ILogger<StateController> _logger;
    private readonly PGApiContext _repository;

    public StateController(ILogger<StateController> logger, PGApiContext repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet]
    public IEnumerable<object> GetAllStates()
    {
        return _repository.States.Select(x => new
        {
            x.Id,
            x.Name,
            x.ShortName,
            x.Country
        });
    }

    [HttpGet]
    [Route("GetStateByShortName")]
    public IEnumerable<Object> GetStateByShortName(string shortName)
    {
        return _repository.States.Where(x => x.ShortName == shortName).Select(x => new
        {
            x.Id,
            x.Name,
            x.ShortName,
            x.Country
        });
    }

    [HttpPost]
    public ActionResult CreateState(StateDTO stateData)
    {
        var country = _repository.Countries.FirstOrDefault(x => x.Id == stateData.CountryId);

        if (country == default)
            return NotFound(new { stateData.CountryId, error = $"There was no country with an id of {stateData.CountryId}." });

        var newState = new State();
        newState.Name = stateData.Name;
        newState.ShortName = stateData.ShortName;
        newState.Country = country;

        _repository.States.Add(newState);
        _repository.SaveChanges();

        return new OkObjectResult(new { Message = "State created successfully!", newState.Id });
    }
}
