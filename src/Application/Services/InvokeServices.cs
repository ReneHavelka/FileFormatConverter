using Application.AssigningTask;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
	//Volanie služieb v slede, ktorý spoločný pre všeky kontrolery.
	public class InvokeServices
	{
		ISourceFileToTemporaryFile _sourceFileToTemporaryFile;
		IFileToConvert _fileToConvert;

		public InvokeServices(ISourceFileToTemporaryFile sourceFileToTemporaryFile, IFileToConvert fileToConvert)
		{
			_sourceFileToTemporaryFile = sourceFileToTemporaryFile;
			_fileToConvert = fileToConvert;
		}

		public byte[] GetFileBytes(IFormFile receivedFile, string fromFileFormat, string toFileFormat)
		{
			//Ulož do dočasného súboru.
			_sourceFileToTemporaryFile.FileToTemporaryFile(receivedFile);

			//Načítaj dáta z dočasného súboru, vyber a uskutočni konverziu.
			var convertedFileBytes = Conversion(fromFileFormat, toFileFormat);

			return convertedFileBytes;
		}

		public byte[] GetFileBytes(string urlLink, string fromFileFormat, string toFileFormat)
		{
			//Ulož do dočasného súboru.
			_sourceFileToTemporaryFile.UrlFileToTemporaryFile(urlLink);

			//Načítaj dáta z dočasného súboru, vyber a uskutočni konverziu.
			var convertedFileBytes = Conversion(fromFileFormat, toFileFormat);

			return convertedFileBytes;
		}

		private byte[] Conversion(string fromFileFormat, string toFileFormat)
		{
			//Načítaj dáta z dočasného súboru.
			//ProtoBuf - načíta dáta zo súboru v objekte triedy pre konvertovanie (ProtoBufToJson).
			string toConvertFile = string.Empty;
			if (fromFileFormat.ToLower() != "protobuf")
			{ 
				toConvertFile = _fileToConvert.GetFileToConvert(); 
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
