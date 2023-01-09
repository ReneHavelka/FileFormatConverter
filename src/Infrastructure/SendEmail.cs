using Application.Common.Interfaces;
using Application.Common.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Infrastructure
{
	public class SendEmail : ISendEmail
	{
		public async Task DispatchEmail(EmailDataDto emailDataDto, EmailConnectionDataDto emailConnectionDataDto, string fileName)
		{
			var email = new MimeMessage();
			email.From.Add(MailboxAddress.Parse(emailConnectionDataDto.UserName));
			email.To.Add(MailboxAddress.Parse(emailDataDto.To));
			email.Subject = emailDataDto.Subject;

			var builder = new BodyBuilder();
			builder.TextBody = emailDataDto.Body;
			builder.Attachments.Add(fileName);
			email.Body = builder.ToMessageBody();

			using var smtp = new SmtpClient();
			smtp.Connect(emailConnectionDataDto.Host, 587, SecureSocketOptions.StartTls);
			smtp.Authenticate(emailConnectionDataDto.UserName, emailConnectionDataDto.Password);
			Task sendAsync = smtp.SendAsync(email);
			await sendAsync;
			smtp.Disconnect(true);
		}
	}
}
