using Application.Common.ConversionList;
using Domain.Common;

namespace Application.AssigningTask
{
	public class AssignConversion
	{
		public IConversionMethod GetConversionAssignment(string fromFileFormat, string toFileFormat)
		{
			fromFileFormat = fromFileFormat.ToUpper();
			toFileFormat = toFileFormat.ToUpper();

			var conversions = new Conversions();
			var conversionList = conversions.ConversionList;

			//Výnimka - nesprávne zvolená konverzia
			try
			{
				conversionList.First(x => x.FromFormat == fromFileFormat && x.ToFormat == toFileFormat);
			}
			catch
			{
				throw new Exception("Nesprávne zadané parametre pre konverziu.");
			}

			//Zvol správnu metódu pre konverziu.
			var conversionMethod = conversionList.First(x => x.FromFormat == fromFileFormat && x.ToFormat == toFileFormat).ConversionMethod;

			return conversionMethod;
		}
	}
}
