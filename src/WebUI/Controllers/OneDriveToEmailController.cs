using Application.AssigningTask;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Services;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph.ExternalConnectors;

namespace WebUI.Controllers
{
	//Stiahnutie súboru z cloud-u onedrive

	[Route("api/[controller]")]
	[ApiController]
	public class OneDriveToEmailController : ControllerBase
	{

		ISourceFileToTemporaryFile _sourceFileToTemporaryFile;
		IFileToConvert _fileToConvert;
		ISaveFileToDisk _saveFileToDisk;
		IConfiguration _configuration;
		ISendEmail _sendEmail;

		public OneDriveToEmailController(ISourceFileToTemporaryFile sourceFileToTemporaryFile, IFileToConvert fileToConvert, ISaveFileToDisk saveFileToDisk,
			ISendEmail sendEmail, IConfiguration configuration)
		{
			_sourceFileToTemporaryFile = sourceFileToTemporaryFile;
			_fileToConvert = fileToConvert;
			_saveFileToDisk = saveFileToDisk;
			_sendEmail = sendEmail;
			_configuration = configuration;
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] string oneDriveLink, [FromForm] string fromFileFormat, 
			[FromForm] string toFileFormat, string toEmailAddress)
		{
			//Získaj URI zo zdielaného linku z onedrive.
			string base64Value = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(oneDriveLink));
			string encodedUrl = "u!" + base64Value.TrimEnd('=').Replace('/', '_').Replace('+', '-');
			string resultUrl = string.Format("https://api.onedrive.com/v1.0/shares/{0}/root/content", encodedUrl);

			//Služby - vyber konverziu, ulož do dočasného súboru pod novým menom (kvôli zlepšeniu bezpečnosti pred malware-om),
			//načítaj dáta z dočasného súboru, konvertuj.
			var commonServices = new InvokeServices(_sourceFileToTemporaryFile, _fileToConvert);
			var convertedFileBytes = commonServices.GetFileBytes(resultUrl, fromFileFormat, toFileFormat);

			//Ulož na disk.
			var fileNameToReturn = await _saveFileToDisk.SaveFileAsync(convertedFileBytes, toFileFormat);

			var emailDetails = new EmailDetails(_configuration, _sendEmail);

			//Zadefinuj jednotlivé prvky štruktúry emailu.
			var emailData = emailDetails.GetEmailData(toEmailAddress);

			//Email connection
			var emailConnection = emailDetails.GetEmailConnectionData();

			//Vytvor email a odošli ho. Host, UserName a Password sú v appsettings.json.
			//Na Webe ethereal.email sa dá skontrolovať, či súbor prišiel aj s prílohou.
			var sendEmail = new SendEmail();
			sendEmail.DispatchEmail(emailData, emailConnection, fileNameToReturn);

			return Ok();
		}
	}
}
