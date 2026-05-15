using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace identiy.app.Controllers;

public class SecretController : Controller
{
    // GET
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public IActionResult Index()
    {                               
        var username = HttpContext.User.Identity.Name;
        ViewBag.Username = username;
    
        var userRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        ViewBag.Role = userRole;
    
        return View();
    }
}