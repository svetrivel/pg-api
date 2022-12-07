using PgAPI.Domain;

public class LoginMessage : ValidationMessage
{
    private LoginMessage(string code, string message) : base(code, message)
    {
    }
    public static LoginMessage UserNotFound => new LoginMessage("user_not_found", "User Not Found");
    public static LoginMessage InvalidEmail => new LoginMessage("invalid_email", "Invalid Email!");
    public static LoginMessage InvalidCredentials => new LoginMessage("invalid_login_credentials", "Entered credentials are incorrect, Please try again!");
    public static LoginMessage ReachedMaxInvalidAttempts => new LoginMessage("login_attempt_exceeds_the_maximum_limit", "You have reached maximum invalid login attempts!");
    public static LoginMessage LoginSuccessful => new LoginMessage("login_success", "You have loggedin successfully!");
    public static LoginMessage UserRegistrationSuccessful => new LoginMessage("user_registration_success", "User created successfully!");
    public static LoginMessage UserRegistrationFailed => new LoginMessage("user_registration_failed", "User registration failed!");
}