namespace Application.Common.Interfaces
{
	//Implementovaný vo vrstve Infrastrucure - uloženie konvertovaného súboru.
	public interface ISaveFileToDisk
	{
		public string SaveFile(byte[] convertedFile, string fileExt);
	}
}
