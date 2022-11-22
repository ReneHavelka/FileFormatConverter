using Application.Common.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
	//Prijatie súboru z formulára prostredníctvom HttpRequest

	[Route("api/[controller]")]
	[ApiController]
	public class FileToFileController : ControllerBase
	{
		ISourceFileToTemporaryFile _sourceFileToTemporaryFile;
		IFileToConvert _fileToConvert;
		public FileToFileController(ISourceFileToTemporaryFile sourceFileToTemporaryFile, IFileToConvert fileToConvert)
		{
			_sourceFileToTemporaryFile = sourceFileToTemporaryFile;
			_fileToConvert = fileToConvert;
		}

		[HttpPost]
		public IActionResult Post([FromForm] IFormFile receivedFile, [FromForm] string fromFileFormat, [FromForm] string toFileFormat)
		{
			//Služby - vyber konverziu, ulož do dočasného súboru pod novým menom (kvôli zlepšeniu bezpečnosti pred malware-om),
			//načítaj dáta z dočasného súboru, konvertuj.
			var commonServices = new InvokeServices(_sourceFileToTemporaryFile, _fileToConvert);
			var convertedFileBytes = commonServices.GetFileBytes(receivedFile, fromFileFormat, toFileFormat);

			var contentType = $"application/{toFileFormat}";
			var file = File(convertedFileBytes, contentType);

			return Ok(file);
		}
	}
}
