﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GimenaCreations.Models;

public class ApplicationUser : IdentityUser
{
    public ICollection<Order> Orders { get; set; }

    [PersonalData, Display(Name = "First name")]
    public string FirstName { get; set; }

    [PersonalData, Display(Name = "Last name")]
    public string LastName { get; set; }

    [PersonalData]
    public Address Address { get; set; }
}