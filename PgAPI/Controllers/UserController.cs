using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PgAPI.Services;


namespace PgAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<PGController> _logger;
    private readonly IUserService _userService;

    public UserController(ILogger<PGController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpGet("users")]
    public ICollection<User> GetAllUsers()
    {
        return _userService.GetAllUsers();
    }

    [HttpPost("Login")]
    public ActionResult Login(UserLoginDTO loginData)
    {
        var loginResult = _userService.Login(loginData.UserName, loginData.Password);

        if (loginResult.Contains("Invalid"))
            return BadRequest(new { error = $"{loginResult} - {loginData.UserName}." });

        return new OkObjectResult(loginResult);
    }

    [HttpPost("Register")]
    public ActionResult Register(RegisterUserDTO registerData)
    {
        var newUser = new User
        {
            Status = UserStatus.Pending,
            FirstName = registerData.UserName,
            UserName = registerData.UserName,
            Email = registerData.Email
        };

        var userId = _userService.Register(newUser, registerData.Password);

        if (userId < 1)
            return BadRequest(new { error = $"User Registration failed - {registerData.UserName}." });

        return new OkObjectResult(new { UserId = userId, Message = "User created Successfully!" });
    }
}