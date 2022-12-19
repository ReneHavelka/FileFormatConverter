using Application.Converting;
using System.Text;

namespace FileFormatConverterTests.ApplicationUnitTests
{
	[TestClass]
	public class XmlToJsonUnitTest
	{
		private const string xml = @"<?xml version='1.0' standalone='no'?>
<root>
  <person id='1'>
  <name>Alan</name>
  <url>http://www.google.com</url>
  </person>
  <person id='2'>
  <name>Louis</name>
  <url>http://www.yahoo.com</url>
  </person>
</root>";


		private const string expected = @"{
  ""?xml"": {
    ""@version"": ""1.0"",
    ""@standalone"": ""no""
  },
  ""root"": {
    ""person"": [
      {
        ""@id"": ""1"",
        ""name"": ""Alan"",
        ""url"": ""http://www.google.com""
      },
      {
        ""@id"": ""2"",
        ""name"": ""Louis"",
        ""url"": ""http://www.yahoo.com""
      }
    ]
  }
}";

		[TestMethod]
		public void XmlToJsonTestMethod()
		{
			var xmlToJson = new XmlToJson().ConvertFormat(xml);
            var xmlToJsonStr = Encoding.Unicode.GetString(xmlToJson);

			Assert.AreEqual(expected, xmlToJsonStr);
		}
	}


}
