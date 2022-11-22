using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace Application.Services
{
	public class EmailDetails
	{
		IConfiguration _configuration;
		ISendEmail _sendEmail;
		

		public EmailDetails(IConfiguration configuration, ISendEmail sendEmail)
		{
			_configuration = configuration;
			_sendEmail = sendEmail;
		}

		public EmailDataDto GetEmailData(string toEmailAddress)
		{
			//Zadefinuj jednotlivé prvky štruktúry emailu.
			try
			{
				new MailAddress(toEmailAddress);
			}
			catch
			{
				throw new Exception("Emailová adresa nemá správny formát.");
			}

			var emailDataDto = new EmailDataDto();
			emailDataDto.Body = "Požadovaný formát súboru je v prílohe.";
			emailDataDto.Subject = "Nový formát súboru";
			emailDataDto.To = toEmailAddress;
			return emailDataDto;
		}

		public EmailConnectionDataDto GetEmailConnectionData()
		{
			EmailConnectionDataDto emailConnectionDataDto = new();
			emailConnectionDataDto.Host = _configuration.GetSection("EmailSettings").GetSection("Host").Value;
			emailConnectionDataDto.UserName = _configuration.GetSection("EmailSettings").GetSection("UserName").Value;
			emailConnectionDataDto.Password = _configuration.GetSection("EmailSettings").GetSection("Password").Value;
			return emailConnectionDataDto;
		}

		





	}
}
