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
        public void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("shawky1mohamed2@gmail.com", "Gv8#kL29@rTf!Xz");
            client.Send("shawky1mohamed2@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
