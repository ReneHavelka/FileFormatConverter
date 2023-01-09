using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.IO.Abstractions;
using WebUI.Controllers;

namespace FileFormatConverterTests.WebUIIntegrationTests
{
	[TestClass]
	public class UrlToDiskIntegrationTest
	{
		string expected = @"C:\Users\Public\Documents\JSON.json";

		[TestMethod]
		public void UrlToDiskTestMethod()
		{
			var receivedFileToSave = new SourceFileToTemporaryFile();
			var fileToConvert = new FileToConvert();
			var saveFileToDisk = new SaveFileToDisk();

			var tempFileName = TemporaryFileName.TemporFileName();
			while (File.Exists(tempFileName)) { }

			var urlToDiskController = new UrlToDiskController(receivedFileToSave, fileToConvert, saveFileToDisk);
			var task = urlToDiskController.Post("https://www.w3schools.blog/wp-content/uploads/c90a6e5686968874bde232437df679e9_176458.xml", "xml", "json");

			var okResult = task.Result as OkObjectResult;

			Assert.AreEqual(expected, okResult.Value);
		}

	}
}
