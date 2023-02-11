using System.ComponentModel.DataAnnotations;

namespace GimenaCreations.Entities;

public class LoginLogoutAudit
{
    public int Id { get; set; }

    [Display(Name = "User ID")]
    public string UserId { get; set; }

    [Display(Name = "Login time")]
    public DateTime? LoginTime { get; set; }

    [Display(Name = "Logout time")]
    public DateTime? LogoutTime { get; set; }
    
    public string Username { get; set; }

    [Display(Name = "Full name")]
    public string FullName { get; set; }
}
