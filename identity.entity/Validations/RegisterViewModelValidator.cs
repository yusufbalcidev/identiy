using FluentValidation;
using identity.entity.ViewModels;

namespace identity.entity.Validations;

public class RegisterViewModelValidator: AbstractValidator<RegisterViewModel>
{
    public RegisterViewModelValidator()
    {
        RuleFor(x => x.Username)
            
            .NotEmpty().WithMessage("Kullanıcı adı boş bırakılamaz.")
            .MinimumLength(3).WithMessage("Kullanıcı adı en az 3 karakter olmalıdır.")
            .MaximumLength(50).WithMessage("Kullanıcı adı 50 karakterden uzun olamaz.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-posta adresi boş bırakılamaz.")
            .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre boş bırakılamaz.")
            .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır."); 
            
        RuleFor(x => x.PasswordConfirm)
            .NotEmpty().WithMessage("Şifre tekrarı boş bırakılamaz.")
            .Equal(x => x.Password).WithMessage("Şifreler birbiriyle uyuşmuyor.");
    }
    
}