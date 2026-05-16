using System.Net;
using System.Net.Mail;

namespace identiy.app.Services;

public class EmailService
{
    private readonly IConfiguration _configuration;
    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task SendResetPasswordEmailAsync(string toEmail, string resetLink)
    {
       
        
        var secretKMail =  _configuration["MailSettings:PrivateMail"];
        var secretPassword =  _configuration["MailSettings:PrivatePassword"];
        

        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            EnableSsl = true,
            UseDefaultCredentials = false, // Bu satırın eklenmesi önemlidir
            Credentials = new NetworkCredential(secretKMail, secretPassword)
        };

        var mailMessage = new MailMessage(from: secretKMail, to: toEmail, subject: "Şifre Sıfırlama Talebi", body: $"<h2>Şifrenizi Sıfırlayın</h2><p>Şifrenizi yenilemek için lütfen <a href='{resetLink}'>buraya tıklayın</a>.</p>");

        

        mailMessage.IsBodyHtml = true;

        await client.SendMailAsync(mailMessage);
    }
}