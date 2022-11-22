using Application.Common.Models;

namespace Application.Common.Interfaces
{
	//Implementovaný vo vrstve Infrastrucure - SendEmail
	public interface ISendEmail
	{
		public void DispatchEmail(EmailDataDto emailDataDto, EmailConnectionDataDto emailConnectionDataDto, string fileName);
	}
}
