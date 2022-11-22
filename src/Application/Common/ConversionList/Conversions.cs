using Application.Converting;
using Domain.Entities;

namespace Application.Common.ConversionList
{
	//Možnosť dopĺňať ďalšíe metódy konverzie do zoznamu
	public class Conversions
	{
		public IList<Conversion> ConversionList = new List<Conversion>();

		public Conversions()
		{
			var conversion = new Conversion("XML", "JSON", new XmlToJson());
			ConversionList.Add(new Conversion("XML", "JSON", new XmlToJson()));
			ConversionList.Add(new Conversion("JSON", "XML", new JsonToXml()));
			ConversionList.Add(new Conversion("JSON", "PROTOBUF", new JsonToProtoBuf()));
			ConversionList.Add(new Conversion("PROTOBUF", "JSON", new ProtoBufToJson()));
		}
	}
}
