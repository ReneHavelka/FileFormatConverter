using Domain.Common;

namespace Domain.Entities
{
	//Použité pri deklarácii typu objektov v zozname - Application.Common.ConversionList.Conversions.cs
	public class Conversion
	{
		public string FromFormat { get; set; }
		public string ToFormat { get; set; }
		public IConversionMethod ConversionMethod { get; set; }

		public Conversion(string fromFormat, string toFormat, IConversionMethod conversionMethod)
		{
			FromFormat = fromFormat;
			ToFormat = toFormat;
			ConversionMethod = conversionMethod;
		}
	}
}
