public class PG
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Address Address { get; set; }

    public ICollection<Room> Rooms { get; set; }
}

public class PGDTO
{
    public string Name { get; set; }
    public int AddressId { get; set; }
}