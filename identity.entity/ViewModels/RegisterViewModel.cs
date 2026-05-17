using System.ComponentModel.DataAnnotations;

namespace identity.entity.ViewModels;

public class RegisterViewModel
{
    
    [Required(ErrorMessage = "bos gecilemez")]
    public string Username { get; set; } = string.Empty;
    [Required(ErrorMessage = "bos gecilemez")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "bos gecilemez")]
    public string Password { get; set; } = string.Empty; 
    [Compare(nameof(Password), ErrorMessage = "Parola eşleşmiyor.")]
    [Required(ErrorMessage = "bos gecilemez")] 
    public string PasswordConfirm { get; set; } = string.Empty;
    

}