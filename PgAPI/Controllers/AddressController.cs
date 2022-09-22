using Microsoft.AspNetCore.Mvc;

namespace PgAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddressController : ControllerBase
{
    private readonly ILogger<AddressController> _logger;
    private readonly PGApiContext _repository;

    public AddressController(ILogger<AddressController> logger, PGApiContext repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet]
    public IEnumerable<Object> Get()
    {
        return _repository.Addresses.Select(x => new { x.Id, x.AddressLine1, x.AddressLine2, x.State, x.City, x.Pincode });
    }

    [HttpPost]
    public ActionResult CreateAddress(AddressDTO addressData)
    {
        var state = _repository.States.FirstOrDefault(x => x.Id == addressData.StateId);

        if (state == default)
            return NotFound(new { StateId = addressData.StateId, error = $"There was no State with an id of {addressData.StateId}." });

        var newAddress = new Address();
        newAddress.AddressLine1 = addressData.AddressLine1;
        newAddress.AddressLine2 = addressData.AddressLine2;
        newAddress.State = state;
        newAddress.City = addressData.City;
        newAddress.Pincode = addressData.Pincode;

        _repository.Addresses.Add(newAddress);
        _repository.SaveChanges();

        return new OkObjectResult(new { Message = "Address created successfully!", Id = newAddress.Id });
    }
}
