using Domain.Common;
using Newtonsoft.Json;
using System.Text;
using System.Xml.Linq;

namespace Application.Converting
{
	public class XmlToJson : IConversionMethod
	{
		//Konverzia z XML na JSON.
		public byte[] ConvertFormat(string str)
		{
			try
			{
				XDocument.Parse(str);
			}
			catch
			{
				throw new Exception("Formát súboru nie je XML.");
			}

			var doc = XDocument.Parse(str);
			var serializedDoc = JsonConvert.SerializeXNode(doc, Formatting.Indented);

			return Encoding.Unicode.GetBytes(serializedDoc);
		}
	}
}
