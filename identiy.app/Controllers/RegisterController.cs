using identity.entity.Model;
using identity.entity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace identiy.app.Controllers;

public class RegisterController : Controller
{
    private readonly UserManager<AppUser> _userManager;

    public RegisterController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        // FluentValidation kurallarından biri bile başarısız olursa IsValid false döner
        if (!ModelState.IsValid)
        {
            // Hatalarla birlikte aynı sayfayı geri döndür
            return View(model); 
        }
        
        var newUser = new AppUser
        {
            UserName = model.Username,
            Email = model.Email
        };


        var result = await _userManager.CreateAsync(newUser, model.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(newUser, "User");
            ViewBag.Message = "Kayıt tamamlandı giriş yapabilirsiniz.";
            return View();
        }


        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }
}