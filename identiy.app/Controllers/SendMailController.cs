using identity.entity.Model;
using identiy.app.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace identiy.app.Controllers;

public class SendMailController : Controller
{
    private readonly UserManager<AppUser> _userManager;

    private readonly EmailService _emailService;

    public SendMailController(UserManager<AppUser> userManager, EmailService emailService)
    {
        _userManager = userManager;
        _emailService = emailService;
    }

    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        
        var user = await _userManager.FindByNameAsync(email);
        if (user == null)
        {
            ViewBag.Message = "Eğer kaydınız varsa e-posta adresinize şifre sıfırlama linki gönderildi.";
            return View();
        }
        
        var token= await _userManager.GeneratePasswordResetTokenAsync(user);
        
       
        var resetLink = Url.Action("ResetPassword", "SendMail", new { email = user.UserName, token = token },
            Request.Scheme);
            
        await _emailService.SendResetPasswordEmailAsync(user.UserName, resetLink);
        
        ViewBag.Message = "Şifre sıfırlama linki e-posta adresinize gonderildi.";
        return View();
    }
    
    [HttpGet]
    public IActionResult ResetPassword(string email, string token)
    {
        if (email == null || token == null) return BadRequest("Geçersiz istek.");
        ViewBag.Email = email;
        ViewBag.Token = token;
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> ResetPassword(string email, string token, string newPassword)
    {
        var user = await _userManager.FindByNameAsync(email);
        if (user == null) return BadRequest("Kullanıcı bulunamadı.");

        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

        if (result.Succeeded)
        {
            TempData["message"] = "Şifreniz başarıyla güncellendi. Yeni şifrenizle giriş yapabilirsiniz.";
            return RedirectToAction("Index", "Login");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        ViewBag.Email = email;
        ViewBag.Token = token;
        return View();
    }
}