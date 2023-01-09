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
			//Imitovanie súboru poslaného prostredníctvom HttpRequest
			var content = xml;
			var fileName = "Test.xml";
			var stream = new MemoryStream();
			var writer = new StreamWriter(stream);
			Task writeAsync = writer.WriteAsync(content);
			await writeAsync;
			Task flushAsync = writer.FlushAsync();
			await flushAsync;
			stream.Position = 0;
			IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);
			//Argumenty pre konštruktor FileToFileController
			var receivedFileToSave = new SourceFileToTemporaryFile();
			var fileToConvert = new FileToConvert();
			//Vytvorenie inštancie z FileToFileController
			var fileToFileController = new FileToFileController(receivedFileToSave, fileToConvert);
			//Volanie metódy Post
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
