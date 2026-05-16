using identity.entity.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace identiy.app.Controllers;


[Authorize(Roles = "Admin")] 
public class AdminController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;

    public AdminController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
       
        var users = _userManager.Users.ToList();
        
       
        var userRoles = new Dictionary<int, string>();
        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            userRoles.Add(user.Id, roles.FirstOrDefault() ?? "Rol Yok");
        }

        
        ViewBag.UserRoles = userRoles;
        ViewBag.AllRoles = _roleManager.Roles.ToList();
        
        return View(users);
    }

    [HttpPost]
    public async Task<IActionResult> ChangeRole(int userId, string newRole)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        
        if (user != null && !string.IsNullOrEmpty(newRole))
        {
           
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            
          
            await _userManager.AddToRoleAsync(user, newRole);
        }
        
        
        return RedirectToAction("Index");
    }
}