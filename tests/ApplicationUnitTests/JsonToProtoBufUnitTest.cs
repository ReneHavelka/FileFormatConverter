using Application.Common.Models.ProtobufNetContracts;
using Application.Converting;
using ProtoBuf;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace FileFormatConverterTests.ApplicationUnitTests
{
	[TestClass]
	public class JsonToProtoBufUnitTest
	{
		private const string json = @"[{'Id':12345,'Name':'Fredy','Address':{'Line1':'Flat 1','Line2':'The Meadows'}},{'Id':12346,'Name':'John','Address':{'Line1':'Flat 2','Line2':'The Meadows'}}]";

		string expected = "John";

		[TestMethod]
		public void JsonToProtoBufUnitMethod()
		{
			var jsonToProtoBuf = new JsonToProtoBuf();
			var jsonToProtobufByteArr = jsonToProtoBuf.ConvertFormat(json);

			IList<Person> people = new List<Person>();
			using (var memoryStream = new MemoryStream(jsonToProtobufByteArr))
			{
				people = Serializer.Deserialize<IList<Person>>(memoryStream);
			}

			var name = people[1].Name;

			Assert.AreEqual("John", name);
		}
	}
}
