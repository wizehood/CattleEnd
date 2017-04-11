using CattleEnd.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CattleEnd.EmailService
{
    public static class EmailService
    {
        public static void SendMail(string warriorName, List<string> mails)
        {
            var message = new MailMessage();
            message.From = new MailAddress(ConfigurationManager.AppSettings["SenderAddress"]);
            foreach (var mail in mails)
            {
                message.To.Add(new MailAddress(mail));
            }
            message.Subject = "Responsible guard for night patrol";
            message.Body = $"This evening's brave hero is: {warriorName}";

            var password = ConfigurationManager.AppSettings["Password"];
            var smtpClient = new SmtpClient
            {
                Host = ConfigurationManager.AppSettings["SmtpHost"],
                Port = Convert.ToInt32(ConfigurationManager.AppSettings["ServerPort"]),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(message.From.Address, password)
            };

            smtpClient.Send(message);
        }
    }
}
