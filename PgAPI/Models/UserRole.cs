public class UserRole
{
    public int Id { get;set; }
    public User User { get; set; }
    public List<Role> Roles { get;set; }
}

public enum Role
{
    SystemAdmin,
    Owner,
    Tenant
}