using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.EmailSender
{
    public interface IEmailSender
    {
        void SendEmail(Email email);
    }
}
