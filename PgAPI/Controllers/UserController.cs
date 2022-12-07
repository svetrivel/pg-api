using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PgAPI.Helpers;
using PgAPI.Services;


namespace PgAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<PGController> _logger;
    private readonly IUserService _userService;
    private readonly IConfiguration _config;

    public UserController(ILogger<PGController> logger, IUserService userService, IConfiguration config)
    {
        _logger = logger;
        _userService = userService;
        _config = config;
    }

    [HttpGet("users")]
    // [Authorize]
    public ICollection<User> GetAllUsers()
    {
        return _userService.GetAllUsers();
    }

    [AllowAnonymous]
    [HttpGet("Ping")]
    public object Test()
    {
        var appSettings = new { PasswordRegex = _config["AppSettings.PasswordRegex"], JwtSecret = _config["JwtSecretKey"] };
        return new OkObjectResult(appSettings);
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public ActionResult Login(UserLoginDTO loginData)
    {
        var user = _userService.GetUser(loginData.Email);

        if (user == default)
            return APIResponse.Create(LoginMessage.UserNotFound, HttpStatusCode.NotFound);

        var loggedIn = _userService.Login(user, loginData.Password);

        if (!loggedIn)
            return APIResponse.CreateFromError(LoginMessage.InvalidCredentials, HttpStatusCode.Unauthorized);

        return APIResponse.Create(LoginMessage.LoginSuccessful);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public ActionResult Register(RegisterUserDTO registerData)
    {
        var newUser = new User
        {
            Status = UserStatus.Pending,
            FirstName = registerData.FirstName,
            LastName = registerData.LastName,
            UserName = registerData.Email,
            Email = registerData.Email
        };

        var userId = _userService.Register(newUser, registerData.Email);

        if (userId < 1)
            return APIResponse.CreateFromError(LoginMessage.UserRegistrationFailed, HttpStatusCode.BadRequest);

        return APIResponse.Create((new { UserId = userId }).Concat(LoginMessage.UserRegistrationSuccessful), HttpStatusCode.Created);
    }
}