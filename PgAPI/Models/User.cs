public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Company Company { get; set; }
    public Address Address { get; set; }
    public UserStatus Status { get; set; }

    public List<UserRole> UserRoles { get; set; }
}

public enum UserStatus
{
    Pending,
    Active,
    InActive
}