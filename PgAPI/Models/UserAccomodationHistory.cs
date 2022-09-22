public class UserAccomodationHistory
{
    public int Id { get; set; }
    public User User { get; set; }
    public DateTime JoinedOn { get; set; }
    public DateTime? LeftOn { get; set; }
}