using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using ZideunMail.Models;
using ZideunMail.Tools;

namespace ZideunMail.Services
{
    public class EmailSender : IEmailSender, IDisposable
    {
        private readonly EmailAccount _emailAccount;
        private readonly SmtpContext _smtpContext;

        public EmailSender(EmailAccount emailAccount)
        {
            _emailAccount = emailAccount;
            _smtpContext = new SmtpContext(emailAccount);
        }


        /// <summary>
        /// Builds System.Net.Mail.Message
        /// </summary>
        /// <param name="original">Message</param>
        /// <returns>System.Net.Mail.Message</returns>        
        protected virtual MailMessage BuildMailMessage(EmailMessage original)
        {
            MailMessage msg = new MailMessage();

            if (string.IsNullOrEmpty(original.Subject))
            {
                throw new Exception("Required subject is missing!");
            }

            msg.Subject = original.Subject;
            msg.IsBodyHtml = original.BodyFormat == MailBodyFormat.Html;

            if (original.AltText.HasValue())
            {
                msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(original.AltText, new ContentType("text/html")));
                msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(original.Body, new ContentType("text/plain")));
            }
            else
            {
                msg.Body = original.Body;
            }

            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.None;

            msg.From = original.From.ToMailAddress();

            msg.To.AddRange(original.To.Where(x => x.Address.HasValue()).Select(x => x.ToMailAddress()));
            msg.CC.AddRange(original.Cc.Where(x => x.Address.HasValue()).Select(x => x.ToMailAddress()));
            msg.Bcc.AddRange(original.Bcc.Where(x => x.Address.HasValue()).Select(x => x.ToMailAddress()));
            msg.ReplyToList.AddRange(original.ReplyTo.Where(x => x.Address.HasValue()).Select(x => x.ToMailAddress()));

            msg.Attachments.AddRange(original.Attachments);

            if (original.Headers != null)
                msg.Headers.AddRange(original.Headers);

            msg.Priority = original.Priority;

            return msg;
        }

        #region IMailSender Members


        public Task SendEmailAsync(EmailMessage message)
        {
            Guard.NotNull(message, nameof(message));

            var client = _smtpContext.SmtpClient;
            ApplySettings(client);
            var msg = this.BuildMailMessage(message);

            return client.SendMailAsync(msg).ContinueWith(t =>
            {
                client.Dispose();
                msg.Dispose();
            });
        }

        private void ApplySettings(SmtpClient client)
        {
            if (_emailAccount == null)
                return;

            var pickupDirLocation = _emailAccount.PickupDirectoryLocation;
            if (pickupDirLocation.HasValue() && client.DeliveryMethod != SmtpDeliveryMethod.SpecifiedPickupDirectory && Path.IsPathRooted(pickupDirLocation))
            {
                client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                client.PickupDirectoryLocation = pickupDirLocation;
                client.EnableSsl = false;
            }
        }

        public void Dispose()
        {
            _smtpContext.Dispose();
        }

        #endregion
    }
}
