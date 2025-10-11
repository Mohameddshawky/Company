using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.EmailSender
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;

            var message = new MailMessage("shawky1mohamed2@gmail.com", email.To, email.Subject, email.Body);
            message.IsBodyHtml=true;        
            //qzumlwlotmyfzdgw
            client.Credentials = new NetworkCredential("shawky1mohamed2@gmail.com", "qzumlwlotmyfzdgw");
            await client.SendMailAsync(message);
        }
    }
}
