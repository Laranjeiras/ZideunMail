# ZideunMail
Simple send email 


    var sender = new EmailSender();

    var emailOut = new EmailAccount
    {
        DisplayName = "Zideun Outlook",
        Email = "zideun@outlook.com",
        EnableSsl = true,
        Host = "smtp.office365.com",
        Password = "password",
        Port = 587,
        Username = "zideun@outlook.com"
    };

    var ctx = new SmtpContext(emailOut);

    var msg = new EmailMessage("zideun@outlook.com", "Title", $"Body","zideun@outlook.com");

    sender.SendEmail(ctx, msg);
