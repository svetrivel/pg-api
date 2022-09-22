namespace PgAPI.Services;

public class UserService : IUserService
{
    private readonly PGApiContext _repository;

    public UserService(PGApiContext repository)
    {
        _repository = repository;
    }

    public string Login(string userName, string password)
    {
        var user = GetUser(userName);

        if (user == default)
            return "Invalid User";

        return VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt) ? "Login Success"! : "Login failed!";
    }

    public long Register(User user, string password)
    {
        if (GetUser(user.UserName) != default)
            return -1;

        CreatePasswordHash(password, out byte[] passwordHash, out var passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        _repository.Users.Add(user);
        _repository.SaveChanges();
        return user.Id;
    }

    private User GetUser(string userName) => _repository.Users.FirstOrDefault(x => x.UserName.ToLower() == userName.ToLower());
    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }
    private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);
        var computedpasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return passwordHash.SequenceEqual(computedpasswordHash);
    }

    public ICollection<User> GetAllUsers()
    {
        return _repository.Users.Select(x => x).ToList();
    }
}