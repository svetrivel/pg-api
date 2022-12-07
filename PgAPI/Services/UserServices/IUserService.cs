namespace PgAPI.Services;

public interface IUserService
{
    public bool Login(User user, string password);
    public long Register(User user, string password);
    public User GetUser(string email);
    public ICollection<User> GetAllUsers();
}