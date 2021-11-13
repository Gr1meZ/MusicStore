using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MusicStore.Business.Service_Models
{
    public class SendEmail
    {
        public static async Task Send(string email, string subject, string text)
    {
            MailAddress from = new MailAddress("smatbec@gmail.com", "Music Store");
            MailAddress to = new MailAddress(email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = subject;
            m.Body = text;
            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from.Address, "123Ter123");
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(m);
            
        }
       
    }
}
    