using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GimenaCreations.Pages.Admin.Roles;

public class IndexModel : PageModel
{
    private readonly RoleManager<IdentityRole> roleManager;

    public IndexModel(RoleManager<IdentityRole> roleManager)
    {
        this.roleManager = roleManager;
    }

    public IQueryable<IdentityRole> Roles { get; set; }

    public void OnGet() => Roles = roleManager.Roles;
}
