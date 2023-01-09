namespace Application.Common.Interfaces
{
	//Interface - implementovaný vo vrstve Infrastrucure pre načítanie dočasného súboru. Dočasný súbor - obdržaný od klienta, uložený pre prípadnú kontrolu na malware.
	public interface IFileToConvert
	{
		public Task<string> GetFileToConvertAsync();
	}
}
