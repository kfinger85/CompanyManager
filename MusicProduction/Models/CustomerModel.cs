namespace MusicProduction.Models
{
    public class Customer
    {
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    // Address properties
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }

    // Navigation property
    public ICollection<Order> Orders { get; set; }
    }

}