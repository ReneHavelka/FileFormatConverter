using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebUI.Controllers;

namespace FileFormatConverterTests.WebUIIntegrationTests
{
	[TestClass]
	public class FileToFileIntegrationTest
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
		public async Task FileToFileTestMethod()
		{
			var content = xml;
			var fileName = "Test.xml";
			var stream = new MemoryStream();
			var writer = new StreamWriter(stream);
			writer.Write(content);
			writer.Flush();
			stream.Position = 0;

			IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

			var receivedFileToSave = new SourceFileToTemporaryFile();
			var fileToConvert = new FileToConvert();

			var fileToFileController = new FileToFileController(receivedFileToSave, fileToConvert);
			var okResultTask = fileToFileController.Post(file, "XmL", "jSON");
			var okResult = await okResultTask;

			var okObjectResult = okResult as OkObjectResult;
			var fileContentResult = okObjectResult.Value as FileContentResult;
			var fileContents = fileContentResult.FileContents;
			var fileContentsStr = Encoding.Unicode.GetString(fileContents);

			Assert.AreEqual(expected, fileContentsStr);
		}
	}
}
