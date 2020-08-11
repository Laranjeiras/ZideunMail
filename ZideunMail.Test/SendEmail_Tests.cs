using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using ZideunMail.Models;
using ZideunMail.Services;

namespace ZideunMail.Test
{
    [TestClass]
    public class SendEmail_Tests
    {
        [TestMethod]
        public async Task SendEmailAsync()
        {

            var emailAccount = new EmailAccount
            {
                DisplayName = "ZideunCo Outlook",
                Email = "zideun@outlook.com",
                EnableSsl = true,
                Host = "smtp.office365.com",
                Password = "password",
                Port = 587,
                Username = "zideun@outlook.com"
            };

            var body = $"Teste de envio de email usando o pacote <b>ZideunMail</b>.<br>Desenvolvido por Alexandre Laranjeiras/ZideunCo";

            var msg = new EmailMessage("laranja22@hotmail.com", "Title", body, "zideun@outlook.com");

            using (var sender = new EmailSender(emailAccount))
            {
                try
                {
                    await sender.SendEmailAsync(msg);
                }
                catch (Exception ex)
                {
                    Assert.Fail(ex.Message);
                }
            }
        }
    }
}
