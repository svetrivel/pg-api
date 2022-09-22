namespace PgAPI.Services;

public interface IUserService
{
    public string Login(string userName, string password);
    public long Register(User user, string password);
    public ICollection<User> GetAllUsers();
}