using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PGRentConfig
{
    [Key]
    public int Id { get;set; }
    public PG PG { get;set; }
    public int SeatCapacity { get; set; }
    public decimal Rent { get; set; }
}