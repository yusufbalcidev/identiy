using System.Net;
using System.Net.Mail;

namespace identiy.app.Services;

public class EmailService
{
    public async Task SendResetPasswordEmailAsync(string toEmail, string resetLink)
    {
        string mail = "yusuf439000@gmail.com"; 
        string pw = "drio sjlb ewkh ntga"; 

        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            EnableSsl = true,
            UseDefaultCredentials = false, // Bu satırın eklenmesi önemlidir
            Credentials = new NetworkCredential(mail, pw)
        };

        var mailMessage = new MailMessage(from: mail, to: toEmail, subject: "Şifre Sıfırlama Talebi", body: $"<h2>Şifrenizi Sıfırlayın</h2><p>Şifrenizi yenilemek için lütfen <a href='{resetLink}'>buraya tıklayın</a>.</p>");
        mailMessage.IsBodyHtml = true;

        await client.SendMailAsync(mailMessage);
    }
}