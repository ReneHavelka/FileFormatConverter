using Application.AssigningTask;

namespace FileFormatConverterTests.ApplicationUnitTests
{
	[TestClass]
	public class AssignConversionUnitTest
	{
		[TestMethod]
		public async Task AssignConversionTestMethod()
		{
			var fromFileFormat = "xMl";
			var toFileFormat = "jSon";

			var assignConversion = new AssignConversion();
			var getConversionAssignment = assignConversion.GetConversionAssignment(fromFileFormat, toFileFormat);
			var conversionClassName = getConversionAssignment.ToString();

			Assert.AreEqual("XmlToJson", getConversionAssignment.GetType().Name);
		}
	}
}
