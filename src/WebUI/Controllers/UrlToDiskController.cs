using Application.AssigningTask;
using Application.Common.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;

namespace WebUI.Controllers
{
	//Stiahnutie súboru z url-adresy

	[Route("api/[controller]")]
	[ApiController]
	public class UrlToDiskController : ControllerBase
	{
		ISourceFileToTemporaryFile _sourceFileToTemporaryFile;
		IFileToConvert _fileToConvert;
		ISaveFileToDisk _saveFileToDisk;
		public UrlToDiskController(ISourceFileToTemporaryFile sourceFileToTemporaryFile, IFileToConvert fileToConvert, ISaveFileToDisk saveFileToDisk)
		{
			_sourceFileToTemporaryFile = sourceFileToTemporaryFile;
			_fileToConvert = fileToConvert;
			_saveFileToDisk = saveFileToDisk;
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] string urlAddress, [FromForm] string fromFileFormat, [FromForm] string toFileFormat)
		{
			//Služby - vyber konverziu, ulož do dočasného súboru pod novým menom (kvôli zlepšeniu bezpečnosti pred malware-om),
			//načítaj dáta z dočasného súboru, konvertuj.
			var commonServices = new InvokeServices(_sourceFileToTemporaryFile, _fileToConvert);
			var convertedFileBytesTask = commonServices.GetFileBytes(urlAddress, fromFileFormat, toFileFormat);
			var convertedFileBytes = await convertedFileBytesTask;

			////Vyber konverziu.
			//var assignConversion = new AssignConversion();
			//var getConversionAssignment = assignConversion.GetConversionAssignment(fromFileFormat, toFileFormat);

			////Ulož do dočasného súboru.
			//await _sourceFileToTemporaryFile.UrlFileToTemporaryFile(urlAddress);

			////Načítaj dáta z dočasného súboru.
			////ProtoBuf - načíta dáta zo súboru v objekte triedy pre konvertovanie (ProtoBufToJson).
			//string toConvertFile = string.Empty;
			//if (fromFileFormat.ToLower() != "protobuf") { toConvertFile = _fileToConvert.GetFileToConvert(); }

			////Konvertuj.
			//var convertedFileBytes = getConversionAssignment.ConvertFormat(toConvertFile);

			//Ulož na disk.
			Task<string> fileNameTask = _saveFileToDisk.SaveFileAsync(convertedFileBytes, toFileFormat);
			var fileNameToReturn = await fileNameTask;

			return Ok(fileNameToReturn);
		}
	}
}
