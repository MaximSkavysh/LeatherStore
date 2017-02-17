using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using Store.Domain.Entities;
using Store.Domain.Abstract;

namespace Store.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "maximgen666@gmail.com";
        public string MailFromAddress = "maximgen666@mail.ru";
        public bool UseSsl = true;
        public string Username = "MySmtpUsername";
        public string Password = "MySmtpPassword";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"c:\store_emails";
    }

    public class EmailOrderProcessor :IOrderProcessor
    {
        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }
        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                    = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod =
                        SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                .AppendLine("New order had processed")
                .AppendLine("----")
                .AppendLine("Goods:");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Thing.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (in all: {2:c}", line.Quantity, line.Thing.Name, subtotal);
                }

                body.AppendFormat("Total price: {0:c}", cart.ComputeTotalValue())
                    .AppendLine("----")
                    .AppendLine("Deliverty:")
                    .AppendLine(shippingDetails.Name)
                    .AppendLine(shippingDetails.Surname)
                    .AppendLine(shippingDetails.Country)
                    .AppendLine(shippingDetails.City)
                    .AppendLine(shippingDetails.Street)
                    .AppendLine(shippingDetails.Flat ?? "")
                    .AppendLine(shippingDetails.Email);
                MailMessage mailMessage = new MailMessage(emailSettings.MailFromAddress, emailSettings.MailToAddress, 
                    "New order deliverd", body.ToString());
                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;

                }

                smtpClient.Send(mailMessage);
            }
        }

    }
}
