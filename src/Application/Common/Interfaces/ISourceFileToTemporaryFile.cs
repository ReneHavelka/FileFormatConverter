using Microsoft.AspNetCore.Http;

namespace Application.Common.Interfaces
{
	//Implementovaný vo vrstve Infrastructure - uloženie súboru od klienta na disk, dve metódy - prijatie súboru z formulára prostredníctvom HttpRequest
	//a stiahnutie súboru z adresy url, resp. cloud-u onedrive.
	public interface ISourceFileToTemporaryFile
	{
		public void FileToTemporaryFile(IFormFile receivedFile);
		public Task UrlFileToTemporaryFile(string urlAddress);
	}
}
