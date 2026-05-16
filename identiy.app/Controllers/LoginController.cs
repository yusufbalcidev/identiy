using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using identity.entity.Model;
using identity.entity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace identiy.app.Controllers;

public class LoginController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
private readonly IConfiguration _configuration;
    public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }


    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(UserViewModel model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);
        
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            
            var userRoles = await _userManager.GetRolesAsync(user);

            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) 
            };

            
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var secretKey =  _configuration["JwtSettings:SecretKey"];
            var key = Encoding.ASCII.GetBytes(secretKey!);
            var symmetricSecurityKey = new SymmetricSecurityKey(key);
            var creds = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims, 
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        
           
            Response.Cookies.Append("JwtTokenxd", tokenString, new CookieOptions
            {
                HttpOnly = true, 
                Expires = DateTime.Now.AddHours(1)
            });

          
            return RedirectToAction("Index", "Secret");
        }

        ViewBag.Message = "Kullanıcı adı veya şifre hatalı";
        return View();
    }

}