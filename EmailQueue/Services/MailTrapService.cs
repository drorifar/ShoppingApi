using System.Net.Mail;
using System.Net;
using EmailQueue.Entities;

namespace EmailQueue.Services
{

    public interface IMailService
    {
        Task SendEmail(EmailMessage message);
    }

    //using the mailtrap site
    public class MailTrapService : IMailService
    {

        public async Task SendEmail(EmailMessage message)
        {
            // code that we copied from mailtrap site when registering with a user 
            var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("2dc7c19435b4c5", "3d986afefb6192"),
                EnableSsl = true
            };

            await client.SendMailAsync(message.From, message.To, message.Subject, message.Body); // we can see the mails in the mailtrap
        }
    }
}
