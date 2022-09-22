using Microsoft.EntityFrameworkCore;

public class PGApiContext : DbContext
{
    public PGApiContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<PG> PGs { get; set; }
    public DbSet<PGConfig> PGConfigs { get; set; }
    public DbSet<PGRentConfig> PGRentConfigs { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserAccomodationHistory> UserAccomodationHistories { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
}