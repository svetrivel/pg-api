namespace PgAPI.Services;

public class UserService : IUserService
{
    private readonly PGApiContext _repository;

    public UserService(PGApiContext repository)
    {
        _repository = repository;
    }

    public bool Login(User user, string password)
    {       
        return VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);
    }

    public long Register(User user, string password)
    {
        if (GetUser(user.Email) != default)
            return -1;

        CreatePasswordHash(password, out byte[] passwordHash, out var passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        _repository.Users.Add(user);
        _repository.SaveChanges();
        return user.Id;
    }

    public User GetUser(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new ArgumentNullException(nameof(email));
        }

        return _repository.Users.FirstOrDefault(x => x.Email.ToLower() == email.ToLower());
    }

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