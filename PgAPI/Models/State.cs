public class State
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public Country Country { get;set; }
}

public class StateDTO
{
    public string Name { get; set; }
    public string ShortName { get; set; }
    public int CountryId { get;set; }
}