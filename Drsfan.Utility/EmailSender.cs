using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Drsfan.Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly string _apiKey = "ce4bcc8f0b9adaa672f9736dc0a83e2e";
        private readonly string _secretKey = "e504d5f5ebf28fa6813e6f97a170c831";

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new MailjetClient(_apiKey, _secretKey);

            var request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
            .Property(Send.FromEmail, "phuoctrong100@gmail.com")
            .Property(Send.FromName, "Drsfan")
            .Property(Send.Subject, subject)
            .Property(Send.HtmlPart, htmlMessage)
            .Property(Send.Recipients, new JArray
            {
                new JObject { { "Email", email } }
            });

            await client.PostAsync(request);
        }
    }
}
