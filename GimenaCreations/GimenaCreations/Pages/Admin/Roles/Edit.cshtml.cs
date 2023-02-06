using GimenaCreations.Models;
using GimenaCreations.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GimenaCreations.Pages.Admin.Roles;

public class EditModel : PageModel
{
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly UserManager<ApplicationUser> userManager;

    public EditModel(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        this.roleManager = roleManager;
        this.userManager = userManager;
    }

    [BindProperty]
    public RoleEdit RoleEdit { get; set; }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        IdentityRole role = await roleManager.FindByIdAsync(id);
        List<ApplicationUser> members = new();
        List<ApplicationUser> nonMembers = new();

        foreach (ApplicationUser user in userManager.Users)
        {
            (await userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers).Add(user);
        }

        RoleEdit = new RoleEdit
        {
            Role = role,
            Members = members,
            NonMembers = nonMembers
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(RoleModification model)
    {
        IdentityResult result;

        if (ModelState.IsValid)
        {
            foreach (string userId in model.AddIds ?? Array.Empty<string>())
            {
                ApplicationUser user = await userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    result = await userManager.AddToRoleAsync(user, model.RoleName);

                    if (!result.Succeeded)
                    {
                        Errors(result);
                    }
                }
            }
            foreach (string userId in model.DeleteIds ?? Array.Empty<string>())
            {
                ApplicationUser user = await userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    result = await userManager.RemoveFromRoleAsync(user, model.RoleName);

                    if (!result.Succeeded)
                    {
                        Errors(result);
                    }
                }
            }
        }

        return ModelState.IsValid ? RedirectToPage("Index") : (IActionResult)RedirectToPage("Edit", model.RoleId);
    }

    private void Errors(IdentityResult result)
    {
        foreach (IdentityError error in result.Errors)
        {
            ModelState.AddModelError("", error.Description);
        }
    }
}
