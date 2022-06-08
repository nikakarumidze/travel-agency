namespace Domain.POCOs;

public class Order
{
    public int Id { get; set; }
    public DateTime IssuedAt { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public bool Approved { get; set; }
    public ApplicationUser Host { get; set; }
    public ApplicationUser Guest { get; set; }
    public Apartment Apartment { get; set; }
    
    public string HostId { get; set; }
    public string GuestId { get; set; }
    public int ApartmentId {get;set;}
}