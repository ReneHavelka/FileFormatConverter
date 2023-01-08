using Application.Common.Interfaces;

namespace Infrastructure
{
	public class SaveFileToDisk : ISaveFileToDisk
	{
		public async Task<string> SaveFileAsync(byte[] convertedFile, string fileFormat)
		{
			var fileName = TemporaryFileName.TemporFileSave();
			var fileExtension = fileFormat.ToLower();
			if (fileExtension == "protobuf") { fileExtension = "bin"; }
			fileName = fileName.Replace("Temp.tmp", $"{fileFormat.ToUpper()}.{fileExtension}");
			await System.IO.File.WriteAllBytesAsync(fileName, convertedFile);
			return fileName;
		}
	}
}
