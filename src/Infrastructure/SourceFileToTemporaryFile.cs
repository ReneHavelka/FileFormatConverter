using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text;
using static Microsoft.Graph.Constants;

namespace Infrastructure
{
	public class SourceFileToTemporaryFile : ISourceFileToTemporaryFile
	{
		readonly string tempFileName;

		public SourceFileToTemporaryFile()
		{
			tempFileName = TemporaryFileName.TemporFileName();
		}

		public void FileToTemporaryFile(IFormFile receivedFile)
		{
			//Ulož prijaté dáta do dočasného súboru pod novým menom (kvôli zlepšeniu bezpečnosti pred malware-om).
			if (receivedFile.Length > 0)
			{
				using (var stream = File.Create(tempFileName))
				{
					receivedFile.CopyTo(stream);
				}
			}
			else
			{
				throw new Exception("Súbor je prázdny.");
			}
		}


		public async Task UrlFileToTemporaryFile(string urlAddress)
		{
			//Ulož prijaté dáta do dočasného súboru.
			var httpClient = new HttpClient();
			var allBytes = httpClient.GetByteArrayAsync(urlAddress).Result;

			await File.WriteAllBytesAsync(tempFileName, allBytes);
		}
	}
}
