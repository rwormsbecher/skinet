namespace Core.OrderAggregate;

public class AddressOrder
{
    public AddressOrder()
    {

    }
    public AddressOrder(string firstName, string lastName, string street, string city, string state, string zipcode)
    {
        FirstName = firstName;
        LastName = lastName;
        Street = street;
        City = city;
        State = state;
        ZipCode = zipcode;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
}
