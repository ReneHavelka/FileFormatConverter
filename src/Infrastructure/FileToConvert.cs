using Application.Common.Interfaces;

namespace Infrastructure
{
	public class FileToConvert : IFileToConvert
	{
		public string GetFileToConvert()
		{
			//Načítaj dáta z dočasného súboru a odovzdaj ich pre ďalšie spracovanie - konvertovanie.
			//Konvertovanie z Protobuf - priamo z dočasného súboru
			string fileToConvert = string.Empty;
			var tempFileName = TemporaryFileName.TemporFileSave();

			using (FileStream fileStream = new(tempFileName, FileMode.Open, FileAccess.Read))
			{
				using (StreamReader reader = new StreamReader(fileStream))
				{
					fileToConvert = reader.ReadToEnd();
				}
			}

			File.Delete(tempFileName);

			return fileToConvert;
		}
	}
}
