using Application.Converting;
using Application.Services;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUI.Controllers;

namespace FileFormatConverterTests.ApplicationUnitTests
{
	[TestClass]
	public class ProtoBuftoJsonUnitTest
	{
		string expected1 = @"C:\Users\Public\Documents\JSON.json";
		string expected2 = @"[{""Id"":12345,""Name"":""Fredy"",""Address"":{""Line1"":""Flat 1"",""Line2"":""The Meadows""}},{""Id"":12346,""Name"":""John"",""Address"":{""Line1"":""Flat 2"",""Line2"":""The Meadows""}}]";

		[TestMethod]
		public void ProtoBufToJsonTestMethod()
		{
			var oneDriveLink = "https://1drv.ms/u/s!AuRMfWWNlIp_kU4XxISZJdDsYOyv?e=l80qlG";
			//Získaj URI zo zdielaného linku z onedrive.
			string base64Value = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(oneDriveLink));
			string encodedUrl = "u!" + base64Value.TrimEnd('=').Replace('/', '_').Replace('+', '-');
			string resultUrl = string.Format("https://api.onedrive.com/v1.0/shares/{0}/root/content", encodedUrl);

			var receivedFileToSave = new SourceFileToTemporaryFile();
			var fileToConvert = new FileToConvert();
			var saveFileToDisk = new SaveFileToDisk();

			var urlToDiskController = new UrlToDiskController(receivedFileToSave, fileToConvert, saveFileToDisk);
			var task = urlToDiskController.Post(resultUrl, "ProtoBuf", "Json");

			var okResult1 = task.Result as OkObjectResult;
			var okResult2 = File.ReadAllBytes(okResult1.Value.ToString());

			Assert.AreEqual(expected1, okResult1.Value);
			Assert.AreEqual(expected2, Encoding.Unicode.GetString(okResult2));
		}
	}
}
