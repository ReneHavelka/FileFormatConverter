namespace Application.Common.Interfaces
{
	//Implementovaný vo vrstve Infrastrucure - uloženie konvertovaného súboru.
	public interface ISaveFileToDisk
	{
		public Task<string> SaveFileAsync(byte[] convertedFile, string fileExt);
	}
}
