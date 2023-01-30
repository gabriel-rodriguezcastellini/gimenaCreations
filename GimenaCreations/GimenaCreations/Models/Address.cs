using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

namespace GimenaCreations.Models;

[Owned]
public class Address
{
    [Required, PersonalData]
    public string Street { get; set; } = null!;

    [Required, PersonalData]
    public string City { get; set; } = null!;

    [Required, PersonalData]
    public string State { get; set; } = null!;

    [Required, PersonalData]
    public string Country { get; set; } = null!;

    [Required, PersonalData]
    public string ZipCode { get; set; } = null!;
}
