﻿using System.ComponentModel.DataAnnotations;

namespace GimenaCreations.Models;

public enum OrderStatus
{
    [Display(Name = "Submited")]
    Submited = 1,

    [Display(Name = "Awaiting validation")]
    AwaitingValidation = 2,    

    [Display(Name = "Paid")]
    Paid = 4,

    [Display(Name = "Shipped")]
    Shipped = 5    
}
