using System.Net;
using System.Net.Mail;

namespace TMS2.API.SendMail;

public class SendMail
{
    public async Task SendError(string toAddress, string subject, string body)
    {
        const string smtpServer = "smtp-auth.mailprotect.be";
        const int smtpPort = 587;
        const string smtpUsername = "application.hooyberghs@jeremycoorevits.be";
        const string smtpPassword = "Admin123!";

        using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
        {
            smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            smtpClient.EnableSsl = true;

            using (MailMessage message = new MailMessage("application.hooyberghs@jeremycoorevits.be", toAddress, subject, body))
            {
                await smtpClient.SendMailAsync(message);
            }
        }
    }
}