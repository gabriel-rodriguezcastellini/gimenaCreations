using Microsoft.AspNetCore.Identity;

namespace GimenaCreations.Entities;

public class Address
{
    [PersonalData]
    public string Street { get; set; }

    [PersonalData]
    public string City { get; set; }

    [PersonalData]
    public string State { get; set; }

    [PersonalData]
    public string Country { get; set; }

    [PersonalData]
    public string ZipCode { get; set; }
}
