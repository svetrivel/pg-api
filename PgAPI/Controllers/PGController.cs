using Microsoft.AspNetCore.Mvc;

namespace PgAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PGController : ControllerBase
{
    private readonly ILogger<PGController> _logger;
    private readonly PGApiContext _repository;

    public PGController(ILogger<PGController> logger, PGApiContext repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet]
    public IEnumerable<object> Get()
    {
        return _repository.PGs.Select(x => new { x.Id, x.Name, x.Address });
    }

    [HttpGet]
    [Route("GetPGByName")]
    public IEnumerable<object> GetPGByName(string name)
    {
        return _repository.PGs.Where(x => x.Name == name).Select(x => new { x.Id, x.Name, x.Address });
    }

    [HttpPost]
    public ActionResult CreatePG(PGDTO pgData)
    {
        var address = _repository.Addresses.FirstOrDefault(x => x.Id == pgData.AddressId);

        if (address == default)
            return NotFound(new { pgData.AddressId, error = $"There was no Address with an id of {pgData.AddressId}." });

        var newPG = new PG();
        newPG.Name = pgData.Name;
        newPG.Address = address;

        _repository.PGs.Add(newPG);
        _repository.SaveChanges();

        return new OkObjectResult(new { Message = "PG created successfully!", Id = newPG.Id });
    }
}
