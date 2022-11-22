using System.IO.Abstractions;

namespace Application.Common.Models
{
	public class EmailDataDto
	{
		public string To { get; set; } = string.Empty;
		public string Subject { get; set; } = string.Empty;
		public string Body { get; set; } = string.Empty;
		public IFile Attachment { get; set; }
	}
}
