using ECommerceApp.DAL.Entities;
using ECommerceApp.BLL.Services.Implementations;
using System.Net;
using System.Net.Mail;

namespace ECommerceApp.PL.Helper
{
    public class EmailSettings
    {

        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.ethereal.email", 587);

            client.EnableSsl = true;

            client.Credentials = new NetworkCredential("chad.terry67@ethereal.email", "5He75M2CrR7126UTjS");

            client.Send("chad.terry67@ethereal.email ", email.To, email.Title, email.Body);

        }

    }
}
