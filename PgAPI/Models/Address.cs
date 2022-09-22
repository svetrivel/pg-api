public class Address
{
    public int Id { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public State State { get; set; }
    public string Pincode { get; set; }
}
public class AddressDTO
{
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public int StateId { get; set; }
    public string Pincode { get; set; }
}