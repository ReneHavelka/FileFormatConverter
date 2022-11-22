using Application.Common.Interfaces;

namespace Infrastructure
{
	public class SaveFileToDisk : ISaveFileToDisk
	{
		public string SaveFile(byte[] convertedFile, string fileFormat)
		{
			var fileName = TemporaryFileName.TemporFileSave();
			var fileExtension = fileFormat.ToLower();
			if (fileExtension == "protobuf") { fileExtension = "bin"; }
			fileName = fileName.Replace("Temp.tmp", $"{fileFormat.ToUpper()}.{fileExtension}");
			System.IO.File.WriteAllBytes(fileName, convertedFile);
			return fileName;
		}
	}
}
