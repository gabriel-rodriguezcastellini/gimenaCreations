using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GimenaCreations.Models;

public class ApplicationUser : IdentityUser
{
    public ICollection<Order> Orders { get; set; } = null!;

    [Required, PersonalData]
    public string FirstName { get; set; } = null!;

    [Required, PersonalData]
    public string LastName { get; set; } = null!;

    [Required, PersonalData]
    public Address Address { get; set; } = null!;
}