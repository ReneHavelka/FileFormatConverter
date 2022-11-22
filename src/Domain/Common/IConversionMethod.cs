namespace Domain.Common
{
	//Implementácia v triedach pre konverziu (Application.Converting). Dôležité pri zadefinovaní zoznamu metód kovertovania a použití zoznamu 
	// - nie je potrebné editovať stávajúci kód, stačí len zadefinovať príslušnú metódu a doplniť do zoznamu (princíp open-close).
	public interface IConversionMethod
	{
		public byte[] ConvertFormat(string str);
	}
}
