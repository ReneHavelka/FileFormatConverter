using Application.AssigningTask;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
	//Volanie služieb v slede, ktorý je spoločný pre všeky kontrolery.
	public class InvokeServices
	{
		ISourceFileToTemporaryFile _sourceFileToTemporaryFile;
		IFileToConvert _fileToConvert;

		public InvokeServices(ISourceFileToTemporaryFile sourceFileToTemporaryFile, IFileToConvert fileToConvert)
		{
			_sourceFileToTemporaryFile = sourceFileToTemporaryFile;
			_fileToConvert = fileToConvert;
		}

		public async Task<byte[]> GetFileBytes(IFormFile receivedFile, string fromFileFormat, string toFileFormat)
		{
			//Ulož do dočasného súboru.
			_sourceFileToTemporaryFile.FileToTemporaryFile(receivedFile);

			//Načítaj dáta z dočasného súboru, vyber a uskutočni konverziu.
			Task<byte[]> convertedFileBytesTask = Conversion(fromFileFormat, toFileFormat);
			byte[] convertedFileBytes = await convertedFileBytesTask;

			return convertedFileBytes;
		}

		public async Task<byte[]> GetFileBytes(string urlLink, string fromFileFormat, string toFileFormat)
		{
			//Ulož do dočasného súboru.
			_sourceFileToTemporaryFile.UrlFileToTemporaryFile(urlLink);

			//Načítaj dáta z dočasného súboru, vyber a uskutočni konverziu.
			Task<byte[]> convertedFileBytesTask = Conversion(fromFileFormat, toFileFormat);
			byte[] convertedFileBytes = await convertedFileBytesTask;

			return convertedFileBytes;
		}

		private async Task<byte[]> Conversion(string fromFileFormat, string toFileFormat)
		{
			//Načítaj dáta z dočasného súboru.
			//ProtoBuf - načíta dáta zo súboru v objekte triedy pre konvertovanie (ProtoBufToJson).
			string toConvertFile = string.Empty;
			if (fromFileFormat.ToLower() != "protobuf")
			{ 
				Task<string> toConvertFileTask = _fileToConvert.GetFileToConvertAsync();
				toConvertFile = await toConvertFileTask;
			}
			else
			{
				string currentFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
				toConvertFile = Path.Combine(currentFolder, "Temp.tmp");
			}

			//Vyber konverziu.
			var assignConversion = new AssignConversion();
			var getConversionAssignment = assignConversion.GetConversionAssignment(fromFileFormat, toFileFormat);

			//Konvertuj.
			var convertedFileBytes = getConversionAssignment.ConvertFormat(toConvertFile);
			return convertedFileBytes;
		}
	}
}
