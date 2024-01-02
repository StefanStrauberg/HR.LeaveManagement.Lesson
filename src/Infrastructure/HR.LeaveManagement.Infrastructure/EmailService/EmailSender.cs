using System.Diagnostics;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Models.Email;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace HR.LeaveManagement.Infrastructure.EmailService;

internal sealed class EmailSender(IOptions<EmailSettings> emailSettings) : IEmailSender
{
    readonly EmailSettings _emailSettings = emailSettings.Value
        ?? throw new ArgumentNullException(nameof(emailSettings));

    async Task<bool> IEmailSender.SendEmailAsync(EmailMessage email)
    {
        var mailMessage = new MimeMessage();

        mailMessage.From.Add(new MailboxAddress(_emailSettings.FromName, 
                                                _emailSettings.FromAddress));
        mailMessage.To.Add(new MailboxAddress(email.ToName, 
                                              email.ToAddress));
        mailMessage.Subject = email.Subject;
        mailMessage.Body = new TextPart(email.Body);

        try
        {
            using var smtpClient = new SmtpClient();
            await smtpClient.ConnectAsync("smtp.gmail.com",
                                          587,
                                          true);
            await smtpClient.AuthenticateAsync("user",
                                               "password");
            await smtpClient.SendAsync(mailMessage);
            await smtpClient.DisconnectAsync(true);

            return true;
        }
        catch (Exception e)
        {
            Debug.Print(e.ToString());
            return false;
        }
    }
}