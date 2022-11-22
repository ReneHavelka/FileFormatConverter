using Domain.Common;
using Newtonsoft.Json;
using System.Text;
using System.Xml.Linq;

namespace Application.Converting
{
	public class JsonToXml : IConversionMethod
	{
		//Konverzia z JSON na XML.
		public byte[] ConvertFormat(string str)
		{
			try
			{
				JsonConvert.DeserializeXNode(str, "Root");
			}
			catch
			{
				throw new Exception("Formát súboru nie je JSON.");
			}

			var doc = JsonConvert.DeserializeXNode(str, "Root");
			var declaration = doc.Declaration ?? new XDeclaration("1.0", null, null);
			string xmlStr = $"{declaration}{Environment.NewLine}{doc}";

			return Encoding.Unicode.GetBytes(xmlStr);
		}
	}
}
