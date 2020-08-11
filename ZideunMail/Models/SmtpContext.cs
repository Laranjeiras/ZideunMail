using System;
using System.Net;
using System.Net.Mail;
using ZideunMail.Tools;

namespace ZideunMail.Models
{
    public class SmtpContext : IDisposable
    {
        public SmtpContext(EmailAccount account)
        {
            Guard.NotNull(account, nameof(account));

            this.Host = account.Host;
            this.Port = account.Port;
            this.EnableSsl = account.EnableSsl;
            this.Password = account.Password;
            this.UseDefaultCredentials = account.UseDefaultCredentials;
            this.Username = account.Username;

            var smtpClient = new SmtpClient(this.Host, this.Port)
            {
                UseDefaultCredentials = this.UseDefaultCredentials,
                EnableSsl = this.EnableSsl,
                Timeout = 10000
            };

            if (this.UseDefaultCredentials)
            {
                smtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
            }
            else
            {
                if (!string.IsNullOrEmpty(this.Username))
                    smtpClient.Credentials = new NetworkCredential(this.Username, this.Password);
            }

        }

        public bool UseDefaultCredentials { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public SmtpClient SmtpClient { get; private set; }

        public void Dispose()
        {
            SmtpClient.Dispose();
        }
    }
}
