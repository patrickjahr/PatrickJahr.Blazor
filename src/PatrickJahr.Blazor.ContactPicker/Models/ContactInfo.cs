public class ContactInfo
{
    public string Name { get; set; } = string.Empty;
    public string Tel { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public IJSObjectReference? Icon { get; set; }
}

public class ContactAddress
{
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string DependentLocality { get; set; } = string.Empty;
    public string Organization { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Recipient { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string[] AddressLine { get; set; } = string.Empty;
}