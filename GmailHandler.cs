using System.Net;
using System.Net.Mail;

namespace GiftMailer
{
    class GmailHandler
    {
        private readonly string FromAddress;
        private readonly string FromFriendlyName;
        private readonly string FromPassword;
        private SmtpClient Client = null;

        public GmailHandler(string address, string friendlyName, string password)
        {
            FromAddress = address;
            FromFriendlyName = friendlyName;
            FromPassword = password;
        }
        private void InitializeSMTPClient()
        {
            if (Client != null)
                return;
            Client = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(FromAddress, FromPassword),
                Timeout = 20000
            };
        }
        public void SendEmail(MailAddress toAddress, string subject, string body)
        {
            InitializeSMTPClient();
            using (var message = new MailMessage(new MailAddress(FromAddress, FromFriendlyName), toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                Client.Send(message);
            }
        }
    }
}
