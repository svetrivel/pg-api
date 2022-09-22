using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PGConfig
{
    [Key]
    public int Id { get;set; }
    public PG PG { get;set; }
    public decimal AcRent { get; set; }
    public decimal Advance { get; set; }
    public decimal Maintenance { get; set; }
    public bool IsActive { get; set; }
    public int NoticePeriodDays { get; set; }
    public bool HasHotWater { get; set; }
    public bool HasParkingArea { get; set; }
}