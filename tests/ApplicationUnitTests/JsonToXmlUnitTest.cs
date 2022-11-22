using Application.Converting;
using System.Text;

namespace FileFormatConverterTests.ApplicationUnitTests
{
	[TestClass]
	public class JsonToXmlUnitTest
	{
		private const string json = @"{
  '@Id': 1,
  'Email': 'james@example.com',
  'Active': true,
  'CreatedDate': '2013-01-20T00:00:00Z',
  'Roles': [
    'User',
    'Admin'
  ],
  'Team': {
    '@Id': 2,
    'Name': 'Software Developers',
    'Description': 'Creators of fine software products and services.'
  }
}";
		private const string expected = @"<?xml version=""1.0""?>
<Root Id=""1"">
  <Email>james@example.com</Email>
  <Active>true</Active>
  <CreatedDate>2013-01-20T00:00:00Z</CreatedDate>
  <Roles>User</Roles>
  <Roles>Admin</Roles>
  <Team Id=""2"">
    <Name>Software Developers</Name>
    <Description>Creators of fine software products and services.</Description>
  </Team>
</Root>";

		[TestMethod]
		public void JsonToXmlTestMethod()
		{
			var jsonToXml = new JsonToXml().ConvertFormat(json);
			var jsonToXmlStr = Encoding.Unicode.GetString(jsonToXml);

			Assert.AreEqual(expected, jsonToXmlStr);
		}
	}

}
