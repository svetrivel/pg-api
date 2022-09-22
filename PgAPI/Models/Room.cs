public class Room
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SeatCapacity { get; set; }
    public bool HasAC { get; set; }
    public decimal Rent { get; set; }
    public PG PG { get; set; }
}