using System.Net;
using System.Net.Mail;

namespace TMS2.API.SendMail;

public class SendMail
{
    public async Task SendError(string toAddress, string subject, string body)
    {
        const string smtpServer = "smtp-relay.sendinblue.com";
        const int smtpPort = 587;
        const string smtpUsername = "jonashacker7@gmail.com";
        const string smtpPassword = "RY9v06yWkw2EGMTH";

        using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
        {
            smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            smtpClient.EnableSsl = true;

            using (MailMessage message = new MailMessage("application@hooyberghs.be", toAddress, subject, body))
            {
                await smtpClient.SendMailAsync(message);
            }
        }
    }
}